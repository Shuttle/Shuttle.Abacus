using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class ArgumentHandler :
        IMessageHandler<RegisterArgumentCommand>,
        IMessageHandler<RenameArgumentCommand>,
        IMessageHandler<RemoveArgumentCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IEventStore _eventStore;

        public ArgumentHandler(IDatabaseContextFactory databaseContextFactory, IEventStore eventStore)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(eventStore, "eventStore");

            _databaseContextFactory = databaseContextFactory;
            _eventStore = eventStore;
        }

        public void ProcessMessage(IHandlerContext<RegisterArgumentCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var argument = new Argument(Guid.NewGuid());
                var stream = new EventStream(argument.Id);

                stream.AddEvent(argument.Register(message.Name, message.AnswerType));

                _eventStore.Save(stream);
            }

            context.ReplyOK();
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
                }
            }

            context.ReplyOK();
        }

        public void ProcessMessage(IHandlerContext<RenameArgumentCommand> context)
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

                if (!argument.IsNamed(message.Name))
                {
                    stream.AddEvent(argument.Rename(message.Name));

                    _eventStore.Save(stream);
                }
            }

            context.ReplyOK();
        }
    }
}