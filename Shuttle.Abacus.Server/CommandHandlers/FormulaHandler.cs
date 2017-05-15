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
        IMessageHandler<RemoveFormulaCommand>,
        IMessageHandler<SetFormulaOperationsCommand>,
        IMessageHandler<SetFormulaConstraintsCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IEventStore _eventStore;
        private readonly IKeyStore _keyStore;

        public FormulaHandler(IDatabaseContextFactory databaseContextFactory, IEventStore eventStore, IKeyStore keyStore)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(eventStore, "eventStore");
            Guard.AgainstNull(keyStore, "keyStore");

            _databaseContextFactory = databaseContextFactory;
            _eventStore = eventStore;
            _keyStore = keyStore;
        }

        public void ProcessMessage(IHandlerContext<RegisterFormulaCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var key = Formula.Key(message.Name);

                if (_keyStore.Contains(key))
                {
                    return;
                }

                var formula = new Formula(Guid.NewGuid());
                var stream = new EventStream(formula.Id);

                stream.AddEvent(formula.Register(message.Name));

                _eventStore.Save(stream);
                _keyStore.Add(formula.Id, key);
            }
        }

        public void ProcessMessage(IHandlerContext<RemoveFormulaCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.FormulaId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var formula = new Formula(message.FormulaId);

                stream.Apply(formula);

                if (formula.Removed)
                {
                    return;
                }

                stream.AddEvent(formula.Remove());

                _eventStore.Save(stream);
                _keyStore.Remove(formula.Id);
            }
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
                var key = Formula.Key(message.Name);

                if (_keyStore.Contains(key))
                {
                    return;
                }

                var stream = _eventStore.Get(message.FormulaId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var formula = new Formula(message.FormulaId);

                stream.Apply(formula);

                if (formula.IsNamed(message.Name))
                {
                    return;
                }

                _keyStore.Remove(Argument.Key(formula.Name));

                stream.AddEvent(formula.Rename(message.Name));

                _eventStore.Save(stream);
                _keyStore.Add(formula.Id, key);
            }
        }

        public void ProcessMessage(IHandlerContext<SetFormulaOperationsCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.FormulaId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var formula = new Formula(message.FormulaId);

                stream.Apply(formula);

                if (formula.Removed)
                {
                    return;
                }

                stream.AddEvent(formula.RemoveOperations());

                foreach (var operation in message.Operations)
                {
                    stream.AddEvent(formula.AddOperation(
                        operation.SequenceNumber,
                        operation.Operation,
                        operation.ValueSource,
                        operation.ValueSelection));
                }

                _eventStore.Save(stream);
            }
        }

        public void ProcessMessage(IHandlerContext<SetFormulaConstraintsCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.FormulaId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var formula = new Formula(message.FormulaId);

                stream.Apply(formula);

                if (formula.Removed)
                {
                    return;
                }

                stream.AddEvent(formula.RemoveConstraints());

                foreach (var operation in message.Constraints)
                {
                    stream.AddEvent(formula.AddConstraint(
                        operation.SequenceNumber,
                        operation.ArgumentName,
                        operation.Comparison,
                        operation.Value));
                }

                _eventStore.Save(stream);
            }
        }
    }
}