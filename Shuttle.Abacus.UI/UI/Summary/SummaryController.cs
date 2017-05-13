using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.UI.Summary
{
    public class SummaryController : WorkItemController, ISummaryController
    {
        public SummaryController(IServiceBus serviceBus, IMessageBus messageBus, ICallbackRepository callbackRepository) 
            : base(serviceBus, messageBus, callbackRepository)
        {
        }
    }
}
