using System;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
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
        IMessageHandler<RemoveFormulaConstraintCommand>,
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

                if (_keyStore.Contains(key))
                {
                    return;
                }

                var formula = new Formula(Guid.NewGuid());
                var stream = _eventStore.CreateEventStream(formula.Id);

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

                stream.AddEvent(formula.AddConstraint(message.Id, message.ArgumentId, message.Comparison, message.Value));

                _eventStore.Save(stream);
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

        public void ProcessMessage(IHandlerContext<RegisterFormulaOperationCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.Id);

                if (stream.IsEmpty)
                {
                    return;
                }

                var formula = stream.Get<Formula>();

                if (formula.Removed)
                {
                    return;
                }

                stream.AddEvent(formula.RemoveOperation(message.Id));

                _eventStore.Save(stream);
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
    }
}