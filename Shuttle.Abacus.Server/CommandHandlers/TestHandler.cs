using System;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
using Shuttle.Esb;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class TestHandler :
        IMessageHandler<RegisterTestCommand>,
        IMessageHandler<RemoveTestCommand>,
        IMessageHandler<SetTestArgumentCommand>,
        IMessageHandler<RemoveTestArgumentCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IEventStore _eventStore;
        private readonly IKeyStore _keyStore;

        public TestHandler(IDatabaseContextFactory databaseContextFactory, IEventStore eventStore, IKeyStore keyStore)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(eventStore, "eventStore");
            Guard.AgainstNull(keyStore, "keyStore");

            _databaseContextFactory = databaseContextFactory;
            _eventStore = eventStore;
            _keyStore = keyStore;
        }

        public void ProcessMessage(IHandlerContext<RegisterTestCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var key = Test.Key(message.Name);

                if (_keyStore.Contains(key))
                {
                    return;
                }

                var test = new Test(Guid.NewGuid());
                var stream = _eventStore.CreateEventStream(test.Id);

                stream.AddEvent(test.Register(message.Name, message.FormulaName, message.ExpectedResult,
                    message.ExpectedResultType, message.Comparison));

                _eventStore.Save(stream);
                _keyStore.Add(test.Id, key);
            }
        }

        public void ProcessMessage(IHandlerContext<RemoveTestArgumentCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.TestId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var test = new Test(message.TestId);

                stream.Apply(test);

                stream.AddEvent(test.RemoveArgument(message.ArgumentName));

                _eventStore.Save(stream);
            }
        }

        public void ProcessMessage(IHandlerContext<RemoveTestCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.TestId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var test = new Test(message.TestId);

                stream.Apply(test);

                if (test.Removed)
                {
                    return;
                }

                stream.AddEvent(test.Remove());

                _eventStore.Save(stream);
                _keyStore.Remove(test.Id);
            }
        }

        public void ProcessMessage(IHandlerContext<SetTestArgumentCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var stream = _eventStore.Get(message.TestId);

                if (stream.IsEmpty)
                {
                    return;
                }

                var test = new Test(message.TestId);

                stream.Apply(test);

                stream.AddEvent(test.SetArgument(message.ArgumentName, message.Value));

                _eventStore.Save(stream);
            }
        }
    }
}