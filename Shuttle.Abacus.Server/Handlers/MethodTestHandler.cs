using System.Diagnostics;
using Abacus.Domain;
using Abacus.Infrastructure;
using Abacus.Messages;
using NServiceBus;

namespace Abacus.Server
{
    public class MethodTestHandler :
        MessageHandler,
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

        public void Handle(ChangeMethodTestCommand message)
        {
            Transacted(
                () =>
                methodTestRepository.Save(
                    methodTestRepository.Get(message.MethodTestId)
                        .ProcessCommand(message)));
        }

        public void Handle(CreateMethodTestCommand message)
        {
            Transacted(() => methodTestRepository.Add(new MethodTest(message)));
        }

        public void Handle(DeleteMethodTestCommand message)
        {
            Transacted(() =>
                {
                    foreach (var id in message.MethodTestIds)
                    {
                        methodTestRepository.Remove(methodTestRepository.Get(id));
                    }
                });
        }

        public void Handle(PrintMethodTestCommand message)
        {
            Transacted
                (uow =>
                    {
                        var testResults = new MethodTestPrintEvent();

                        foreach (var id in message.MethodTestIds)
                        {
                            var sw = new Stopwatch();

                            sw.Start();

                            var test = methodTestRepository.Get(id);

                            var context = GetContext(uow, test);

                            sw.Stop();

                            context.Log();
                            context.Log("test case completed in {0} ms",
                                        sw.ElapsedMilliseconds.ToString("#,##0"));

                            testResults.RunEvents.Add(new MethodTestRunEvent
                                                                {
                                                                    WorkItemId = message.WorkItemId,
                                                                    MethodTestId = id,
                                                                    MethodTestDescription = test.Description,
                                                                    ExpectedResult = test.ExpectedResult,
                                                                    MethodContext = context.DTO()
                                                                }
                                );
                        }

                        Bus.Reply(testResults);
                    }
                );
        }

        public void Handle(RunMethodTestCommand message)
        {
            Transacted
                (uow =>
                    {
                        uow.WillUseFullObjectGraph();

                        foreach (var id in message.MethodTestIds)
                        {
                            var sw = new Stopwatch();

                            sw.Start();

                            var test = methodTestRepository.Get(id);

                            var context = GetContext(uow, test);

                            sw.Stop();

                            context.Log();
                            context.Log("test case completed in {0} ms",
                                        sw.ElapsedMilliseconds.ToString("#,##0"));

                            Bus.Reply
                                (new MethodTestRunEvent
                                 {
                                     WorkItemId = message.WorkItemId,
                                     MethodTestId = id,
                                     MethodTestDescription = test.Description,
                                     ExpectedResult = test.ExpectedResult,
                                     MethodContext = context.DTO()
                                 }
                                );
                        }
                    }
                );
        }

        private MethodContext GetContext(IUnitOfWork uow, MethodTest test)
        {
            uow.WillUseNothing();
            uow.WillUseFullObjectGraph();

            var method = uow.Get<Method>(test.MethodId);

            uow.WillUseNothing();
            uow.WillUse<Argument>();

            var arguments = argumentRepository.All();

            var context = new MethodContext(method.MethodName, calculationLoggerFactory.Create());

            test.Answers.ForEach(
                answer =>
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
                    });

            if (context.LoggerEnabled)
            {
                context.Log();
            }

            method.Calculate(context);

            return context;
        }
    }
}
