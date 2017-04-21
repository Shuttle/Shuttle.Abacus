using Abacus.Application;
using Abacus.Domain;
using Abacus.Infrastructure;
using Abacus.Messages;
using NServiceBus;

namespace Abacus.Server
{
    public class LimitHandler :
        MessageHandler,
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

        public void Handle(ChangeLimitCommand message)
        {
            Transacted(() =>
                {
                    var limit = limitFactoryProvider.Get(message.Type).Create(message.Name);

                    limit.AssignId(message.LimitId);

                    limitRepository.Save(limit);
                });
        }

        public void Handle(CreateLimitCommand message)
        {
            Transacted(() =>
                {
                    var owner = RepositoryProvider.Get(message.OwnerName).Get<ILimitOwner>(message.OwnerId);

                    var limit = limitFactoryProvider.Get(message.Type).Create(message.Name);

                    owner.AddLimit(limit);

                    TaskFactory.Create<ICreateLimitTask>().Execute(new OwnerModel(owner, limit));
                });
        }

        public void Handle(DeleteLimitCommand message)
        {
            Transacted(() => limitRepository.Remove(limitRepository.Get(message.LimitId)));
        }
    }
}
