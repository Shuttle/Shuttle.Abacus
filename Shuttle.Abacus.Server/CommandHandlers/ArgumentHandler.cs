using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class ArgumentHandler :
        IMessageHandler<CreateArgumentCommand>,
        IMessageHandler<ChangeArgumentCommand>,
        IMessageHandler<DeleteArgumentCommand>
    {
        private readonly IArgumentRepository _argumentRepository;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        public ArgumentHandler(IDatabaseContextFactory databaseContextFactory, IArgumentRepository argumentRepository)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentRepository, "argumentRepository");

            _databaseContextFactory = databaseContextFactory;
            _argumentRepository = argumentRepository;
        }

        public void ProcessMessage(IHandlerContext<ChangeArgumentCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var argument = new Argument(message.ArgumentId, message.Name, message.AnswerType);

                argument.AddValues(message.Answers);

                _argumentRepository.Save(argument);
            }

            context.ReplyOK();
        }

        public void ProcessMessage(IHandlerContext<CreateArgumentCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var argument = new Argument(Guid.NewGuid(), message.Name, message.AnswerType);

                argument.AddValues(message.Answers);

                _argumentRepository.Add(argument);
            }

            context.ReplyOK();
        }

        public void ProcessMessage(IHandlerContext<DeleteArgumentCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                _argumentRepository.Remove(message.ArgumentId);
            }

            context.ReplyOK();
        }
    }
}