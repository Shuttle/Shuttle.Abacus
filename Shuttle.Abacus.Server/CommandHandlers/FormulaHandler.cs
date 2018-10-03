using System;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
using Shuttle.Esb;
using Shuttle.Recall;
using Shuttle.Recall.Sql.Storage;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class FormulaHandler :
        IMessageHandler<RegisterFormulaCommand>,
        IMessageHandler<RenameFormulaCommand>,
        IMessageHandler<RegisterFormulaMaxmimumCommand>,
        IMessageHandler<RegisterFormulaMinimumCommand>,
        IMessageHandler<RemoveFormulaCommand>,
        IMessageHandler<RemoveFormulaConstraintCommand>,
        IMessageHandler<RemoveFormulaOperationCommand>,
        IMessageHandler<RegisterFormulaOperationCommand>,
        IMessageHandler<RegisterFormulaConstraintCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IEventStore _eventStore;
        private readonly IKeyStore _keyStore;

        public FormulaHandler(IDatabaseContextFactory databaseContextFactory, IEventStore eventStore,
            IKeyStore keyStore)
        {
            Guard.AgainstNull(databaseContextFactory, nameof(databaseContextFactory));
            Guard.AgainstNull(eventStore, nameof(eventStore));
            Guard.AgainstNull(keyStore, nameof(keyStore));

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
                var existingId = _keyStore.Get(key);

                if (!message.Id.Equals(existingId ?? message.Id))
                {
                    return;
                }

                var stream = _eventStore.Get(message.Id);
                var formula = new Formula(stream.Id);

                stream.Apply(formula);

                _keyStore.Remove(Formula.Key(formula.Name));

                stream.AddEvent(formula.Register(message.Name));

                _eventStore.Save(stream);
                _keyStore.Add(formula.Id, key);
            }
        }

        public void ProcessMessage(IHandlerContext<RegisterFormulaConstraintCommand> context)
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

                stream.AddEvent(
                    formula.RegisterConstraint(message.Id.Equals(Guid.Empty) ? Guid.NewGuid() : message.Id, message.ArgumentId, message.Comparison, message.Value));

                _eventStore.Save(stream);
            }
        }

        public void ProcessMessage(IHandlerContext<RegisterFormulaMaxmimumCommand> context)
        {
            throw new NotImplementedException();
        }

        public void ProcessMessage(IHandlerContext<RegisterFormulaMinimumCommand> context)
        {
            throw new NotImplementedException();
        }

        public void ProcessMessage(IHandlerContext<RegisterFormulaOperationCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.FormulaId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var formula = stream.Get<Formula>();

                if (formula.Removed)
                {
                    return;
                }

                stream.AddEvent(formula.RegisterOperation(message.Id.Equals(Guid.Empty) ? Guid.NewGuid() : message.Id,
                    message.Operation, message.ValueProviderName, message.InputParameter));

                _eventStore.Save(stream);
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

        public void ProcessMessage(IHandlerContext<RemoveFormulaConstraintCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.FormulaId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var formula = stream.Get<Formula>();

                if (formula.Removed)
                {
                    return;
                }

                if (!formula.ContainsConstraint(message.ConstraintId))
                {
                    return;
                }

                stream.AddEvent(formula.RemoveConstraint(message.ConstraintId));

                _eventStore.Save(stream);
            }
        }

        public void ProcessMessage(IHandlerContext<RemoveFormulaOperationCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.FormulaId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var formula = stream.Get<Formula>();

                if (formula.Removed)
                {
                    return;
                }

                if (!formula.ContainsOperation(message.OperationId))
                {
                    return;
                }

                stream.AddEvent(formula.RemoveOperation(message.OperationId));

                _eventStore.Save(stream);
            }
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

                _keyStore.Remove(Formula.Key(formula.Name));

                stream.AddEvent(formula.Rename(message.Name));

                _eventStore.Save(stream);
                _keyStore.Add(formula.Id, key);
            }
        }
    }
}