using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class FormulaHandler :
        IMessageHandler<RegisterFormulaCommand>,
        IMessageHandler<RenameFormulaCommand>,
        IMessageHandler<SetFormulaMaxmimumCommand>,
        IMessageHandler<SetFormulaMinimumCommand>,
        IMessageHandler<RemoveFormulaCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IEventStore _eventStore;

        public FormulaHandler(IDatabaseContextFactory databaseContextFactory, IEventStore eventStore)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");

            _databaseContextFactory = databaseContextFactory;
            _eventStore = eventStore;
        }

        public void ProcessMessage(IHandlerContext<RegisterFormulaCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var formula = new Formula(Guid.NewGuid());
                var stream = new EventStream(formula.Id);

                stream.AddEvent(formula.Register(message.Name));

                _eventStore.Save(stream);
            }

            context.ReplyOK();
        }

        public void ProcessMessage(IHandlerContext<RemoveFormulaCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.FormulaId);
                var formula = new Formula(message.FormulaId);

                stream.Apply(formula);

                if (!formula.Removed)
                {
                    stream.AddEvent(formula.Remove());

                    _eventStore.Save(stream);
                }
            }

            context.ReplyOK();
        }

        public void ProcessMessage(IHandlerContext<SetFormulaMaxmimumCommand> context)
        {
            throw new NotImplementedException();
        }

        public void ProcessMessage(IHandlerContext<SetFormulaMinimumCommand> context)
        {
            throw new NotImplementedException();
        }

        public void ProcessMessage(IHandlerContext<RenameFormulaCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.FormulaId);
                var formula = new Formula(message.FormulaId);

                stream.Apply(formula);

                if (!formula.IsNamed(message.Name))
                {
                    stream.AddEvent(formula.Rename(message.Name));

                    _eventStore.Save(stream);
                }
            }

            context.ReplyOK();
        }
    }
}