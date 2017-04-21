using System.Diagnostics;
using Shuttle.Abacus.Domain;
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
        private readonly ICalculationLoggerFactory calculationLoggerFactory;
        private readonly IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider;
        private readonly IArgumentRepository argumentRepository;
        private readonly IMethodTestRepository methodTestRepository;

        public MethodTestHandler(IMethodTestRepository methodTestRepository, ICalculationLoggerFactory calculationLoggerFactory, IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider, IArgumentRepository argumentRepository)
        {
            this.argumentRepository = argumentRepository;
            this.methodTestRepository = methodTestRepository;
            this.argumentRepository = argumentRepository;
            this.calculationLoggerFactory = calculationLoggerFactory;
            this.argumentAnswerFactoryProvider = argumentAnswerFactoryProvider;
        }

        private MethodContext GetContext(MethodTest test)
        {
            var method = methodTestRepository.Get<Method>(test.MethodId);

            var arguments = argumentRepository.All();

            var context = new MethodContext(method.MethodName, calculationLoggerFactory.Create());

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
                        argumentAnswerFactoryProvider.Get(answer.AnswerType).Create(
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

        public void ProcessMessage(IHandlerContext<CreateMethodTestCommand> context)
        {
            methodTestRepository.Add(new MethodTest(context.Message));
        }

        public void ProcessMessage(IHandlerContext<DeleteMethodTestCommand> context)
        {
            foreach (var id in context.Message.MethodTestIds)
            {
                methodTestRepository.Remove(methodTestRepository.Get(id));
            }
        }

        public void ProcessMessage(IHandlerContext<ChangeMethodTestCommand> context)
        {
            var message = context.Message;

            methodTestRepository.Save(
                methodTestRepository.Get(message.MethodTestId)
                    .ProcessCommand(message));
        }

        public void ProcessMessage(IHandlerContext<RunMethodTestCommand> context)
        {
            var message = context.Message;

            foreach (var id in message.MethodTestIds)
            {
                var sw = new Stopwatch();

                sw.Start();

                var test = methodTestRepository.Get(id);

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
                }, c=>c.Reply());
            }
        }

        public void ProcessMessage(IHandlerContext<PrintMethodTestCommand> context)
        {
            var message = context.Message;

            var testResults = new MethodTestPrintEvent();

            foreach (var id in message.MethodTestIds)
            {
                var sw = new Stopwatch();

                sw.Start();

                var test = methodTestRepository.Get(id);

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
}
