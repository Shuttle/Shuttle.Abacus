using System.Collections.Generic;
using Abacus.Application;
using Abacus.Domain;
using Abacus.Infrastructure;
using Abacus.Messages;
using NServiceBus;

namespace Abacus.Server
{
    public class SystemUserHandler :
        MessageHandler,
        IMessageHandler<CreateSystemUserCommand>,
        IMessageHandler<SetPermissionsCommand>,
        IMessageHandler<ChangeLoginNameCommand>,
        IMessageHandler<LoginCommand>
    {
        private readonly ISystemUserRepository repository;

        public SystemUserHandler(ISystemUserRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(ChangeLoginNameCommand message)
        {
            Transacted(
                () =>
                TaskFactory.Create<IChangeLoginNameTask>().Execute(
                    repository.Get(message.SystemUserId).ProcessCommand(message)));
        }

        public void Handle(CreateSystemUserCommand message)
        {
            Transacted(() => repository.Add(new SystemUser(message)));
        }

        public void Handle(LoginCommand message)
        {
            Transacted(() =>
                {
                    var user = repository.FetchByLoginName(message.LoginName);

                    if (user == null)
                    {
                        repository.Add(new SystemUser
                                       {
                                           LoginName = message.LoginName
                                       });

                        Bus.Reply(new ReplyMessage(Result.Create().AddSuccessMessage(
                                                       string.Format(
                                                           "Your login name '{0}' is new and has been added to the security store.  Please contact the Abacus System Administrator to assign permissions to you.",
                                                           message.LoginName))));
                    }
                    else
                    {
                        var permissions = new List<Permission>();

                        user.Permissions.ForEach(permission => permissions.Add((Permission)permission));

                        Bus.Reply(new LoginCompletedEvent { Permissions = permissions });
                    }
                });
        }

        public void Handle(SetPermissionsCommand message)
        {
            Transacted(
                () =>
                TaskFactory.Create<ISetPermissionsTask>().Execute(
                    repository.Get(message.SystemUserId).ProcessCommand(message)));
        }
    }
}
