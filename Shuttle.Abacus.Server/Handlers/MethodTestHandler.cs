using System.Diagnostics;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.Handlers
{
    public class MethodTestHandler :
        IMessageHandler<CreateMethodTestCommand>,
        IMessageHandler<DeleteMethodTestCommand>,
        IMessageHandler<ChangeMethodTestCommand>,
        IMessageHandler<RunMethodTestCommand>,
        IMessageHandler<PrintMethodTestCommand>
    {
        private readonly IFactoryProvider<IArgumentAnswerFactory> _argumentAnswerFactoryProvider;
        private readonly IArgumentRepository _argumentRepository;
        private readonly ICalculationLoggerFactory _calculationLoggerFactory;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IMethodTestRepository _methodTestRepository;

        public MethodTestHandler(IDatabaseContextFactory databaseContextFactory,
            IMethodTestRepository methodTestRepository, ICalculationLoggerFactory calculationLoggerFactory,
            IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider,
            IArgumentRepository argumentRepository)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(methodTestRepository, "methodTestRepository");
            Guard.AgainstNull(calculationLoggerFactory, "calculationLoggerFactory");
            Guard.AgainstNull(argumentAnswerFactoryProvider, "argumentAnswerFactoryProvider");
            Guard.AgainstNull(argumentRepository, "argumentRepository");

            _databaseContextFactory = databaseContextFactory;
            _methodTestRepository = methodTestRepository;
            _argumentRepository = argumentRepository;
            _calculationLoggerFactory = calculationLoggerFactory;
            _argumentAnswerFactoryProvider = argumentAnswerFactoryProvider;
        }

        public void ProcessMessage(IHandlerContext<ChangeMethodTestCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                _methodTestRepository.Save(
                    _methodTestRepository.Get(message.MethodTestId)
                        .ProcessCommand(message));
            }
        }

        public void ProcessMessage(IHandlerContext<CreateMethodTestCommand> context)
        {
            using (_databaseContextFactory.Create())
            {
                _methodTestRepository.Add(new MethodTest(context.Message));
            }
        }

        public void ProcessMessage(IHandlerContext<DeleteMethodTestCommand> context)
        {
            using (_databaseContextFactory.Create())
            {
                foreach (var id in context.Message.MethodTestIds)
                {
                    _methodTestRepository.Remove(id);
                }
            }
        }

        public void ProcessMessage(IHandlerContext<PrintMethodTestCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var testResults = new MethodTestPrintEvent();

                foreach (var id in message.MethodTestIds)
                {
                    var sw = new Stopwatch();

                    sw.Start();

                    var test = _methodTestRepository.Get(id);

                    var methodContext = GetContext(test);

                    sw.Stop();

                    methodContext.Log();
                    methodContext.Log("test case completed in {0} ms",
                        sw.ElapsedMilliseconds.ToString("#,##0"));

                    testResults.RunEvents.Add(new MethodTestRunEvent
                        {
                            WorkItemId = message.WorkItemId,
                            MethodTestId = id,
                            MethodTestDescription = test.Description,
                            ExpectedResult = test.ExpectedResult,
                            MethodContext = methodContext.DTO()
                        }
                    );
                }

                context.Send(testResults, c => c.Reply());
            }
        }

        public void ProcessMessage(IHandlerContext<RunMethodTestCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                foreach (var id in message.MethodTestIds)
                {
                    var sw = new Stopwatch();

                    sw.Start();

                    var test = _methodTestRepository.Get(id);

                    var methodContext = GetContext(test);

                    sw.Stop();

                    methodContext.Log();
                    methodContext.Log("test case completed in {0} ms",
                        sw.ElapsedMilliseconds.ToString("#,##0"));

                    context.Send(new MethodTestRunEvent
                    {
                        WorkItemId = message.WorkItemId,
                        MethodTestId = id,
                        MethodTestDescription = test.Description,
                        ExpectedResult = test.ExpectedResult,
                        MethodContext = methodContext.DTO()
                    }, c => c.Reply());
                }
            }
        }

        private MethodContext GetContext(MethodTest test)
        {
            using (_databaseContextFactory.Create())
            {
                var method = _methodTestRepository.Get<Method>(test.MethodId);

                var arguments = _argumentRepository.All();

                var context = new MethodContext(method.MethodName, _calculationLoggerFactory.Create());

                foreach (var answer in test.Answers)
                {
                    var argument = arguments.Find(answer.ArgumentId);

                    if (argument == null)
                    {
                        context.Log(
                            "Could not find argument '{0}' in the master list.  The answer will be ignored.",
                            answer.ArgumentName);
                    }
                    else
                    {
                        context.AddArgumentAnswer(
                            _argumentAnswerFactoryProvider.Get(answer.AnswerType).Create(
                                argument.Name,
                                answer.Answer));
                    }
                }

                if (context.LoggerEnabled)
                {
                    context.Log();
                }

                method.Calculate(context);

                return context;
            }
        }
    }
}