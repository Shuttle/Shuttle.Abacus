using log4net.Util;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class TestExecutionHandler : IMessageHandler<ExecuteTestCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IExecutionServiceFactory _executionServiceFactory;
        private readonly ITestRepository _testRepository;

        public TestExecutionHandler(IDatabaseContextFactory databaseContextFactory, IExecutionServiceFactory executionServiceFactory, ITestRepository testRepository)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(executionServiceFactory, "executionServiceFactory");
            Guard.AgainstNull(testRepository, "testRepository");

            _databaseContextFactory = databaseContextFactory;
            _executionServiceFactory = executionServiceFactory;
            _testRepository = testRepository;
        }

        public void ProcessMessage(IHandlerContext<ExecuteTestCommand> context)
        {
            if (string.IsNullOrEmpty(context.TransportMessage.SenderInboxWorkQueueUri))
            {
                return;
            }

            using (_databaseContextFactory.Create())
            {
                var test = _testRepository.Get(context.Message.Id);

                var executionContext = _executionServiceFactory.Create().Execute(test.FormulaName, test.ArgumentValues());

                context.Send(new TestExecutedEvent
                {
                    Id = test.Id,
                    FormulaName = test.FormulaName,
                    Result = executionContext.Result()
                }, c=> c.Reply());
            }
        }
    }
}