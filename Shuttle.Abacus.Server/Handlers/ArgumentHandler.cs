using System;
using System.Linq;
using System.Text;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Messages;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class ArgumentHandler :
        IMessageHandler<CreateArgumentCommand>,
        IMessageHandler<ChangeArgumentCommand>,
        IMessageHandler<DeleteArgumentCommand>
    {
        private readonly IArgumentRepository _argumentRepository;
        private readonly IConstraintRepository _constraintRepository;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMethodTestQuery _methodTestQuery;
        private readonly IMethodTestRepository _methodTestRepository;

        public ArgumentHandler(IDatabaseContextFactory databaseContextFactory, IArgumentRepository argumentRepository,
            IMethodTestRepository methodTestRepository, IConstraintRepository constraintRepository, IMethodTestQuery methodTestQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentRepository, "argumentRepository");
            Guard.AgainstNull(methodTestRepository, "methodTestRepository");
            Guard.AgainstNull(constraintRepository, "constraintRepository");
            Guard.AgainstNull(methodTestQuery, "methodTestQuery");

            _databaseContextFactory = databaseContextFactory;
            _argumentRepository = argumentRepository;
            _methodTestRepository = methodTestRepository;
            _constraintRepository = constraintRepository;
            _methodTestQuery = methodTestQuery;
        }

        public void ProcessMessage(IHandlerContext<ChangeArgumentCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var argument = _argumentRepository.Get(message.ArgumentId);
                var nameChanged = !argument.Name.Equals(message.Name, StringComparison.InvariantCultureIgnoreCase);
                var answerTypeChanged =
                    !argument.AnswerType.Equals(message.AnswerType, StringComparison.InvariantCultureIgnoreCase);

                argument.ProcessCommand(message);

                _argumentRepository.Save(argument);

                if (nameChanged)
                {
                    _methodTestRepository.SetArgumentName(message.ArgumentId, message.Name);
                }

                if (answerTypeChanged)
                {
                    _methodTestRepository.SetArgumentAnswerType(message.ArgumentId, message.AnswerType);
                }
            }

            context.ReplyOK();
        }

        public void ProcessMessage(IHandlerContext<CreateArgumentCommand> context)
        {
            using (_databaseContextFactory.Create())
            {
                var message = context.Message;

                _argumentRepository.Add(new Argument().ProcessCommand(message));
            }

            context.ReplyOK();
        }

        public void ProcessMessage(IHandlerContext<DeleteArgumentCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var rows = _methodTestQuery.AllUsingArgument(message.ArgumentId).ToList();

                if (rows.Any())
                {
                    var argument = _argumentRepository.Find(message.ArgumentId);

                    var list = new StringBuilder();

                    foreach (var row in rows)
                    {
                        list.AppendLine(MethodTestColumns.Description.MapFrom(row));
                    }

                    throw new InvalidOperationException(
                        string.Format(
                            "Cannot delete argument '{0}' since it is being used by the following test cases:\r\n{1}",
                            argument.Name, list));
                }

                _argumentRepository.Remove(message.ArgumentId);
            }

            context.ReplyOK();
        }
    }
}