using System;
using System.Collections.Generic;
using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Messages.v1.TransferObjects;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class SystemUserHandler :
        IMessageHandler<CreateSystemUserCommand>,
        IMessageHandler<SetPermissionsCommand>,
        IMessageHandler<ChangeLoginNameCommand>,
        IMessageHandler<LoginCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly ITaskFactory _taskFactory;

        public SystemUserHandler(IDatabaseContextFactory databaseContextFactory,
            ISystemUserRepository systemUserRepository, ITaskFactory taskFactory)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(systemUserRepository, "systemUserRepository");
            Guard.AgainstNull(taskFactory, "taskFactory");

            _databaseContextFactory = databaseContextFactory;
            _systemUserRepository = systemUserRepository;
            _taskFactory = taskFactory;
        }

        public void ProcessMessage(IHandlerContext<ChangeLoginNameCommand> context)
        {
            throw new NotImplementedException();
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                //_taskFactory.Create<IChangeLoginNameTask>().Execute(
                //    _systemUserRepository.Get(message.SystemUserId).ProcessCommand(message));
            }
        }

        public void ProcessMessage(IHandlerContext<CreateSystemUserCommand> context)
        {
            throw new NotImplementedException();
            using (_databaseContextFactory.Create())
            {
                //_systemUserRepository.Add(new SystemUser(context.Message));
            }
        }

        public void ProcessMessage(IHandlerContext<LoginCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
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
                    user.Permissions.ForEach(
                        permission =>
                            permissions.Add(new Permission
                            {
                                Identifier = permission.Identifier,
                                Description = permission.Description
                            }));
                }

                context.Send(new LoginCompletedEvent { Permissions = permissions }, c => c.Reply());
            }
        }

        public void ProcessMessage(IHandlerContext<SetPermissionsCommand> context)
        {
            throw new NotImplementedException(); var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                //_taskFactory.Create<ISetPermissionsTask>().Execute(
                //    _systemUserRepository.Get(message.SystemUserId).ProcessCommand(message));
            }
        }
    }
}