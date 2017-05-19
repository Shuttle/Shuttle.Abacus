using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Test;
using Shuttle.Abacus.Shell.UI.TestArgument;
using Shuttle.Esb;

namespace Shuttle.Abacus.Shell.UI.Test
{
    public class TestController : WorkItemController, ITestController
    {
        public TestController(IServiceBus serviceBus, IMessageBus messageBus)
            : base(serviceBus, messageBus)
        {
        }

        public void HandleMessage(RegisterTestMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<ITestView>();

            var command = new RegisterTestCommand
            {
                Name = view.NameValue,
                FormulaName = view.FormulaNameValue,
                ExpectedResult = view.ExpectedResultValue,
                ExpectedResultType = view.ExpectedResultTypeValue,
                Comparison = view.ComparisonValue
            };

            Send(command);
        }

        public void HandleMessage(RegisterTestArgumentValueMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<ITestArgumentValueView>();

            var command = new RegisterTestArgumentValueCommand
            {
                TestId = message.TestId,
                ArgumentName = view.ArgumentName,
                Value = view.ValueValue
            };

            Send(command);
        }
    }
}