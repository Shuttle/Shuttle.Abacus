using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.WorkItemControllers
{
    public class SummaryController : WorkItemController, ISummaryController
    {
        public SummaryController(IServiceBus serviceBus, IMessageBus messageBus) : base(serviceBus, messageBus)
        {
        }
    }
}
