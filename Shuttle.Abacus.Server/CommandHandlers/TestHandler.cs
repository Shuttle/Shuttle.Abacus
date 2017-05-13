using System;
using System.Diagnostics;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server.CommandHandlers
{
    public class TestHandler :
        IMessageHandler<CreateTestCommand>,
        IMessageHandler<DeleteTestCommand>,
        IMessageHandler<ChangeTestCommand>,
        IMessageHandler<RunTestCommand>,
        IMessageHandler<PrintTestCommand>
    {
        private readonly IArgumentRepository _argumentRepository;
        private readonly ICalculationLoggerFactory _calculationLoggerFactory;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly ITestRepository _testRepository;

        public TestHandler(IDatabaseContextFactory databaseContextFactory,
            ITestRepository testRepository, ICalculationLoggerFactory calculationLoggerFactory,
            IArgumentRepository argumentRepository)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(testRepository, "testRepository");
            Guard.AgainstNull(calculationLoggerFactory, "calculationLoggerFactory");
            Guard.AgainstNull(argumentRepository, "argumentRepository");

            _databaseContextFactory = databaseContextFactory;
            _testRepository = testRepository;
            _argumentRepository = argumentRepository;
            _calculationLoggerFactory = calculationLoggerFactory;
        }

        public void ProcessMessage(IHandlerContext<ChangeTestCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var test = new Test(message.MethodTestId, message.MethodId, message.Description, message.ExpectedResult, message.ExpectedResultType, message.ComparisonType);

                foreach (var answer in message.ArgumentAnswers)
                {
                    test.AddArgumentAnswer(new TestArgumentValue(answer.ArgumentId, answer.Answer));
                }

                _testRepository.Save(test);
            }
        }

        public void ProcessMessage(IHandlerContext<CreateTestCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var test = new Test(message.MethodTestId, message.MethodId, message.Description, message.ExpectedResult, message.ExpectedResultType, message.ComparisonType);

                foreach (var answer in message.ArgumentAnswers)
                {
                    test.AddArgumentAnswer(new TestArgumentValue(answer.ArgumentId, answer.Answer));
                }

                _testRepository.Add(test);
            }
        }

        public void ProcessMessage(IHandlerContext<DeleteTestCommand> context)
        {
            using (_databaseContextFactory.Create())
            {
                foreach (var id in context.Message.MethodTestIds)
                {
                    _testRepository.Remove(id);
                }
            }
        }

        public void ProcessMessage(IHandlerContext<PrintTestCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                var testResults = new MethodTestPrintEvent();

                foreach (var id in message.MethodTestIds)
                {
                    var sw = new Stopwatch();

                    sw.Start();

                    var test = _testRepository.Get(id);

                    var methodContext = GetContext(test);

                    sw.Stop();

                    methodContext.Log();
                    methodContext.Log("test case completed in {0} ms",
                        sw.ElapsedMilliseconds.ToString("#,##0"));

                    testResults.RunEvents.Add(new TestRunEvent
                        {
                            WorkItemId = message.WorkItemId,
                            MethodTestId = id,
                            MethodTestDescription = test.Description,
                            ExpectedResult = test.ExpectedResult,
                            //TODO
                            //FormulaContext = methodContext.DTO()
                        }
                    );
                }

                context.Send(testResults, c => c.Reply());
            }
        }

        public void ProcessMessage(IHandlerContext<RunTestCommand> context)
        {
            var message = context.Message;

            using (_databaseContextFactory.Create())
            {
                foreach (var id in message.MethodTestIds)
                {
                    var sw = new Stopwatch();

                    sw.Start();

                    var test = _testRepository.Get(id);

                    var methodContext = GetContext(test);

                    sw.Stop();

                    methodContext.Log();
                    methodContext.Log("test case completed in {0} ms",
                        sw.ElapsedMilliseconds.ToString("#,##0"));

                    context.Send(new TestRunEvent
                    {
                        WorkItemId = message.WorkItemId,
                        MethodTestId = id,
                        MethodTestDescription = test.Description,
                        ExpectedResult = test.ExpectedResult,
                        //TODO
                        //FormulaContext = methodContext.DTO()
                    }, c => c.Reply());
                }
            }
        }

        private FormulaContext GetContext(Test test)
        {
            throw new InvalidOperationException();
            //using (_databaseContextFactory.Create())
            //{
            //    var method = _testRepository.Get<Formula>(test.FormulaId);

            //    var arguments = _argumentRepository.All();

            //    var context = new FormulaContext((string) method.MethodName);

            //    foreach (var answer in test.ArgumentValues)
            //    {
            //        var argument = arguments.Find(answer.ArgumentName);

            //        //TODO
            //        //if (argument == null)
            //        //{
            //        //    context.Log(
            //        //        "Could not find argument '{0}' in the master list.  The answer will be ignored.",
            //        //        answer.ArgumentName);
            //        //}
            //        //else
            //        //{
            //        //    context.AddValue(
            //        //        _argumentValueFactoryProvider.Get(answer.ValueType).Create(
            //        //            argument.Name,
            //        //            answer.Answer));
            //        //}
            //    }

            //    if (context.LoggerEnabled)
            //    {
            //        context.Log();
            //    }

            //    method.Calculate(context);

            //    return context;
            //}
        }
    }
}