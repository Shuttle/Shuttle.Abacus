using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class LimitHandler :
        IMessageHandler<CreateLimitCommand>,
        IMessageHandler<ChangeLimitCommand>,
        IMessageHandler<DeleteLimitCommand>
    {
        private readonly IFactoryProvider<ILimitFactory> _limitFactoryProvider;
        private readonly ILimitRepository _limitRepository;
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly ITaskFactory _taskFactory;

        public LimitHandler(IFactoryProvider<ILimitFactory> limitFactoryProvider, ILimitRepository limitRepository,
            IRepositoryProvider repositoryProvider, ITaskFactory taskFactory)
        {
            _limitFactoryProvider = limitFactoryProvider;
            _limitRepository = limitRepository;
            _repositoryProvider = repositoryProvider;
            _taskFactory = taskFactory;
        }

        public void ProcessMessage(IHandlerContext<ChangeLimitCommand> context)
        {
            var message = context.Message;

            var limit = _limitFactoryProvider.Get(message.Type).Create(message.Name);

            limit.AssignId(message.LimitId);

            _limitRepository.Save(limit);
        }

        public void ProcessMessage(IHandlerContext<CreateLimitCommand> context)
        {
            var message = context.Message;

            var owner = _repositoryProvider.Get(message.OwnerName).Get<ILimitOwner>(message.OwnerId);

            var limit = _limitFactoryProvider.Get(message.Type).Create(message.Name);

            owner.AddLimit(limit);

            _taskFactory.Create<ICreateLimitTask>().Execute(new OwnerModel(owner, limit));
        }

        public void ProcessMessage(IHandlerContext<DeleteLimitCommand> context)
        {
            var message = context.Message;

            _limitRepository.Remove(_limitRepository.Get(message.LimitId));
        }
    }
}