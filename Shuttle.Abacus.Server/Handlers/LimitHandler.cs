using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class LimitHandler :
        IMessageHandler<CreateLimitCommand>,
        IMessageHandler<ChangeLimitCommand>,
        IMessageHandler<DeleteLimitCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly ILimitRepository _limitRepository;

        public LimitHandler(IDatabaseContextFactory databaseContextFactory, ILimitRepository limitRepository)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(limitRepository, "limitRepository");

            _databaseContextFactory = databaseContextFactory;
            _limitRepository = limitRepository;
        }

        public void ProcessMessage(IHandlerContext<ChangeLimitCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var limit = _limitRepository.Get(message.LimitId);

                limit.ProcessCommand(message);

                _limitRepository.Save(limit);
            }
        }

        public void ProcessMessage(IHandlerContext<CreateLimitCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                _limitRepository.Add(message.OwnerName, message.OwnerId, new Limit(message.Name, message.Type));
            }
        }

        public void ProcessMessage(IHandlerContext<DeleteLimitCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                _limitRepository.Remove(message.LimitId);
            }
        }
    }
}