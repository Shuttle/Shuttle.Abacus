using System;
using System.Data;
using System.Text;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Domain;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class ArgumentHandler :
        IMessageHandler<CreateArgumentCommand>,
        IMessageHandler<ChangeArgumentCommand>,
        IMessageHandler<DeleteArgumentCommand>
    {
        private readonly IMethodTestRepository methodTestRepository;
        private readonly IConstraintRepository constraintRepository;
        private readonly IMethodTestQuery testQuery;
        private readonly IArgumentRepository argumentRepository;

        public ArgumentHandler(IArgumentRepository argumentRepository, IMethodTestRepository methodTestRepository,
            IConstraintRepository constraintRepository, IMethodTestQuery testQuery)
        {
            this.argumentRepository = argumentRepository;
            this.methodTestRepository = methodTestRepository;
            this.constraintRepository = constraintRepository;
            this.testQuery = testQuery;
        }

        public void ProcessMessage(IHandlerContext<CreateArgumentCommand> context)
        {
            argumentRepository.Add(new Argument(context.Message));
        }

        public void ProcessMessage(IHandlerContext<ChangeArgumentCommand> context)
        {
            var message = context.Message;

            var argument = argumentRepository.Get(message.ArgumentId);
            var nameChanged = !argument.Name.Equals(message.Name, StringComparison.InvariantCultureIgnoreCase);
            var answerTypeChanged =
                !argument.AnswerType.Equals(message.AnswerType, StringComparison.InvariantCultureIgnoreCase);

            argument.ProcessCommand(message);

            argumentRepository.Save(argument);

            if (nameChanged)
            {
                constraintRepository.SetArgumentName(message.ArgumentId, message.Name);
                methodTestRepository.SetArgumentName(message.ArgumentId, message.Name);
            }

            if (answerTypeChanged)
            {
                constraintRepository.SetArgumentAnswerType(message.ArgumentId, message.AnswerType);
                methodTestRepository.SetArgumentAnswerType(message.ArgumentId, message.AnswerType);
            }
        }

        public void ProcessMessage(IHandlerContext<DeleteArgumentCommand> context)
        {
            var message = context.Message;
            var testQueryResult = testQuery.AllUsingArgument(message.ArgumentId);
            var argument = argumentRepository.Get(message.ArgumentId);

            if (testQueryResult.Table.Rows.Count > 0)
            {
                var list = new StringBuilder();

                foreach (DataRow row in testQueryResult.Table.Rows)
                {
                    list.AppendLine(MethodTestColumns.Description.MapFrom(row));
                }

                throw new InvalidOperationException(
                    string.Format(
                        "Cannot delete argument '{0}' since it is being used by the following test cases:\r\n{1}",
                        argument.Name, list));
            }

            argumentRepository.Remove(argument);
        }
    }
}

