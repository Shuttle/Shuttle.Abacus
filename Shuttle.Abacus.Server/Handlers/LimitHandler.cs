using Shuttle.Abacus.ApplicationService;
using Shuttle.Abacus.Domain;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class LimitHandler :
        IMessageHandler<CreateLimitCommand>,
        IMessageHandler<ChangeLimitCommand>,
        IMessageHandler<DeleteLimitCommand>
    {
        private readonly IFactoryProvider<ILimitFactory> limitFactoryProvider;
        private readonly ILimitRepository limitRepository;

        public LimitHandler(IFactoryProvider<ILimitFactory> limitFactoryProvider, ILimitRepository limitRepository)
        {
            this.limitFactoryProvider = limitFactoryProvider;
            this.limitRepository = limitRepository;
        }

        public void ProcessMessage(IHandlerContext<ChangeLimitCommand> context)
        {
            var message = context.Message;

            var limit = limitFactoryProvider.Get(message.Type).Create(message.Name);

            limit.AssignId(message.LimitId);

            limitRepository.Save(limit);
        }

        public void ProcessMessage(IHandlerContext<CreateLimitCommand> context)
        {
            var message = context.Message;

            var owner = _repositoryProvider.Get(message.OwnerName).Get<ILimitOwner>(message.OwnerId);

            var limit = limitFactoryProvider.Get(message.Type).Create(message.Name);

            owner.AddLimit(limit);

            _taskFactory.Create<ICreateLimitTask>().Execute(new OwnerModel(owner, limit));
        }

        public void ProcessMessage(IHandlerContext<DeleteLimitCommand> context)
        {
            var message = context.Message;

            limitRepository.Remove(limitRepository.Get(message.LimitId));
        }
    }
}