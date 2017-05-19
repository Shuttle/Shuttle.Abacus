using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Esb;

namespace Shuttle.Abacus.Shell.UI.Summary
{
    public class SummaryController : WorkItemController, ISummaryController
    {
        public SummaryController(IServiceBus serviceBus, IMessageBus messageBus) 
            : base(serviceBus, messageBus)
        {
        }
    }
}
