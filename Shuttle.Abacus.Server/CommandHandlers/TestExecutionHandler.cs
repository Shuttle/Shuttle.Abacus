using System;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class TestExecutionHandler : IMessageHandler<ExecuteTestCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IExecutionService _executionService;
        private readonly ITestRepository _testRepository;

        public TestExecutionHandler(IDatabaseContextFactory databaseContextFactory,
            IExecutionService executionService, ITestRepository testRepository)
        {
            Guard.AgainstNull(databaseContextFactory, nameof(databaseContextFactory));
            Guard.AgainstNull(executionService, nameof(executionService));
            Guard.AgainstNull(testRepository, nameof(testRepository));

            _databaseContextFactory = databaseContextFactory;
            _executionService = executionService;
            _testRepository = testRepository;
        }

        public void ProcessMessage(IHandlerContext<ExecuteTestCommand> context)
        {
            if (string.IsNullOrEmpty(context.TransportMessage.SenderInboxWorkQueueUri))
            {
                return;
            }

            var message = context.Message;

            if (!Enum.TryParse(message.LogLevel, true, out ContextLogLevel logLevel))
            {
                logLevel = ContextLogLevel.None;
            }

            using (_databaseContextFactory.Create())
            {
                var test = _testRepository.Get(message.Id);

                var executionContext = _executionService
                    .Execute(message.Id, test.ArgumentValues(), new ContextLogger(logLevel));

                var response = new TestExecutedEvent
                {
                    Id = test.Id,
                    FormulaId= test.FormulaId,
                    Log = executionContext.Logger.ToString()
                };

                if (!executionContext.HasException)
                {
                    response.Exception = executionContext.Exception.Message;
                    response.FormulaContext = new Messages.v1.TransferObjects.FormulaContext();

                    response.FormulaContext = GetFormulaContext(executionContext.RootFormulaContext);
                }
                else
                {
                    response.Result = executionContext.Result();
                }

                context.Send(response, c => c.Reply());
            }
        }

        private Messages.v1.TransferObjects.FormulaContext GetFormulaContext(FormulaContext formulaContext)
        {
            var responseFormulaContext =
                new Messages.v1.TransferObjects.FormulaContext
                {
                    Result = formulaContext.Result,
                    DateStarted = formulaContext.DateStarted,
                    DateCompleted = formulaContext.DateCompleted
                };

            foreach (var usedArgumentValue in formulaContext.UsedArgumentValues())
            {
                responseFormulaContext.ArgumentAnswers.Add(new Messages.v1.TransferObjects.ArgumentValue
                {
                    Id = usedArgumentValue.Id,
                    Value = usedArgumentValue.Value
                });
            }

            foreach (var containedFormulaContext in formulaContext.ContainedFormulaContexts())
            {
                responseFormulaContext.FormulaContexts.Add(GetFormulaContext(containedFormulaContext));
            }

            return responseFormulaContext;
        }
    }
}