using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
using Shuttle.Esb;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class ArgumentHandler :
        IMessageHandler<RegisterArgumentCommand>,
        IMessageHandler<RegisterArgumentValueCommand>,
        IMessageHandler<RenameArgumentCommand>,
        IMessageHandler<RemoveArgumentCommand>,
        IMessageHandler<RemoveArgumentValueCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IEventStore _eventStore;
        private readonly IKeyStore _keyStore;

        public ArgumentHandler(IDatabaseContextFactory databaseContextFactory, IEventStore eventStore,
            IKeyStore keyStore)
        {
            Guard.AgainstNull(databaseContextFactory, nameof(databaseContextFactory));
            Guard.AgainstNull(eventStore, nameof(eventStore));
            Guard.AgainstNull(keyStore, nameof(keyStore));

            _databaseContextFactory = databaseContextFactory;
            _eventStore = eventStore;
            _keyStore = keyStore;
        }

        public void ProcessMessage(IHandlerContext<RegisterArgumentCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var key = Argument.Key(message.Name);

                if (_keyStore.Contains(key))
                {
                    return;
                }

                var stream = _eventStore.CreateEventStream();
                var argument = new Argument(stream.Id);

                stream.AddEvent(argument.Register(message.Name, message.AnswerType));

                _eventStore.Save(stream);
                _keyStore.Add(argument.Id, key);
            }
        }

        public void ProcessMessage(IHandlerContext<RegisterArgumentValueCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.ArgumentId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var argument = new Argument(message.ArgumentId);

                stream.Apply(argument);

                if (!argument.ContainsValue(message.Value))
                {
                    stream.AddEvent(argument.AddValue(message.Value));

                    _eventStore.Save(stream);
                }
            }
        }

        public void ProcessMessage(IHandlerContext<RemoveArgumentCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.ArgumentId);
                var argument = new Argument(message.ArgumentId);

                stream.Apply(argument);

                if (!argument.Removed)
                {
                    stream.AddEvent(argument.Remove());

                    _eventStore.Save(stream);
                    _keyStore.Remove(message.ArgumentId);
                }
            }
        }

        public void ProcessMessage(IHandlerContext<RemoveArgumentValueCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.ArgumentId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var argument = new Argument(message.ArgumentId);

                stream.Apply(argument);

                if (!argument.ContainsValue(message.Value))
                {
                    return;
                }

                stream.AddEvent(argument.RemoveValue(message.Value));

                _eventStore.Save(stream);
            }
        }

        public void ProcessMessage(IHandlerContext<RenameArgumentCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var key = Argument.Key(message.Name);

                if (_keyStore.Contains(key))
                {
                    return;
                }

                var stream = _eventStore.Get(message.ArgumentId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var argument = new Argument(message.ArgumentId);

                stream.Apply(argument);

                if (argument.IsNamed(message.Name))
                {
                    return;
                }

                _keyStore.Remove(Argument.Key(argument.Name));

                stream.AddEvent(argument.Rename(message.Name));

                _keyStore.Add(message.ArgumentId, key);

                _eventStore.Save(stream);
            }
        }
    }
}