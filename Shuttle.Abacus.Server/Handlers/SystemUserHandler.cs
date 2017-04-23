using System.Collections.Generic;
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
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly ITaskFactory _taskFactory;

        public SystemUserHandler(ISystemUserRepository systemUserRepository, ITaskFactory taskFactory)
        {
            _systemUserRepository = systemUserRepository;
            _taskFactory = taskFactory;
        }

        public void ProcessMessage(IHandlerContext<ChangeLoginNameCommand> context)
        {
            var message = context.Message;

            _taskFactory.Create<IChangeLoginNameTask>().Execute(
                _systemUserRepository.Get(message.SystemUserId).ProcessCommand(message));
        }

        public void ProcessMessage(IHandlerContext<CreateSystemUserCommand> context)
        {
            _systemUserRepository.Add(new SystemUser(context.Message));
        }

        public void ProcessMessage(IHandlerContext<LoginCommand> context)
        {
            var message = context.Message;

            var user = _systemUserRepository.FetchByLoginName(message.LoginName);

            var permissions = new List<Permission>();

            if (user == null)
            {
                _systemUserRepository.Add(new SystemUser
                {
                    LoginName = message.LoginName
                });
            }
            else
            {
                user.Permissions.ForEach(permission => permissions.Add((Permission) permission));

            }

            context.Send(new LoginCompletedEvent { Permissions = permissions }, c => c.Reply());
        }

        public void ProcessMessage(IHandlerContext<SetPermissionsCommand> context)
        {
            var message = context.Message;

            _taskFactory.Create<ISetPermissionsTask>().Execute(
                _systemUserRepository.Get(message.SystemUserId).ProcessCommand(message));
        }
    }
}