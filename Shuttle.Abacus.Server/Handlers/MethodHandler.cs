using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class MethodHandler :
        IMessageHandler<CreateMethodCommand>,
        IMessageHandler<CopyMethodCommand>,
        IMessageHandler<ChangeMethodCommand>,
        IMessageHandler<DeleteMethodCommand>
    {
        private readonly ILimitRepository _limitRepository;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMethodRepository _methodRepository;
        private readonly ITaskFactory _taskFactory;

        public MethodHandler(IDatabaseContextFactory databaseContextFactory, IMethodRepository methodRepository, ILimitRepository limitRepository, ITaskFactory taskFactory)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(methodRepository, "methodRepository");
            Guard.AgainstNull(limitRepository, "limitRepository");
            Guard.AgainstNull(taskFactory, "taskFactory");

            _databaseContextFactory = databaseContextFactory;
            _methodRepository = methodRepository;
            _limitRepository = limitRepository;
            _taskFactory = taskFactory;
        }

        public void ProcessMessage(IHandlerContext<ChangeMethodCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                _taskFactory.Create<IChangeMethodTask>().Execute(
                    _methodRepository.Get(message.MethodId).ProcessCommand(message));
            }
        }

        public void ProcessMessage(IHandlerContext<CopyMethodCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var method = _methodRepository
                    .Get(message.MethodId)
                    .Copy()
                    .ProcessCommand(message);

                _taskFactory.Create<IAddMethodGraphTask>().Execute(method);
            }
        }

        public void ProcessMessage(IHandlerContext<CreateMethodCommand> context)
        {
            using (_databaseContextFactory.Create())
            {
                _methodRepository.Add(new Method(context.Message));
            }
        }

        public void ProcessMessage(IHandlerContext<DeleteMethodCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var method = _methodRepository.Get(message.MethodId);

                foreach (var limit in method.Limits)
                {
                    _limitRepository.Remove(limit.Id);
                }

                _methodRepository.Remove(method.Id);
            }
        }
    }
}