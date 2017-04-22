using System.Collections.Generic;
using Abacus.Messages;
using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class SystemUserHandler :
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

        public void ProcessMessage(IHandlerContext<CreateSystemUserCommand> context)
        {
            repository.Add(new SystemUser(context.Message));
        }

        public void ProcessMessage(IHandlerContext<SetPermissionsCommand> context)
        {
            var message = context.Message;

            _taskFactory.Create<ISetPermissionsTask>().Execute(
                repository.Get(message.SystemUserId).ProcessCommand(message));
        }

        public void ProcessMessage(IHandlerContext<ChangeLoginNameCommand> context)
        {
            var message = context.Message;

            _taskFactory.Create<IChangeLoginNameTask>().Execute(
                repository.Get(message.SystemUserId).ProcessCommand(message));
        }

        public void ProcessMessage(IHandlerContext<LoginCommand> context)
        {
            var message = context.Message;

            var user = repository.FetchByLoginName(message.LoginName);

            if (user == null)
            {
                repository.Add(new SystemUser
                {
                    LoginName = message.LoginName
                });

                context.Send(new ReplyMessage(Result.Create().AddSuccessMessage(
                                               string.Format(
                                                   "Your login name '{0}' is new and has been added to the security store.  Please contact the Abacus System Administrator to assign permissions to you.",
                                                   message.LoginName))), c => c.Reply());
            }
            else
            {
                var permissions = new List<Permission>();

                user.Permissions.ForEach(permission => permissions.Add((Permission)permission));

                context.Send(new LoginCompletedEvent { Permissions = permissions }, c => c.Reply());
            }
        }
    }
}
