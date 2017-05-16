using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Test;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.UI.Test
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
                              ExpectedResult = view.ExpectedResultValue,
                              ExpectedResultType = view.ExpectedResultTypeValue,
                              Comparison = view.ComparisonValue
                          };

            Send(command);
        }

        public void HandleMessage(ChangeTestMessage message)
        {
            var view = WorkItem.GetView<ITestView>();

            var command = new ChangeTestCommand
                          {
                              MethodTestId = message.MethodTestId,
                              MethodId = message.MethodId,
                              Description = view.NameValue,
                              ExpectedResult = view.ExpectedResultValue
                          };

            Send(command);
        }
    }
}
