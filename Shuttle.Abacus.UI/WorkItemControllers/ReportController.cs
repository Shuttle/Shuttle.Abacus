using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.WorkItemControllers.Interfaces;
using Shuttle.Esb;

namespace Shuttle.Abacus.Shell.WorkItemControllers
{
    public class ReportController : WorkItemController, IReportController
    {
        public ReportController(IServiceBus serviceBus, IMessageBus messageBus) 
            : base(serviceBus, messageBus)
        {
        }
    }
}
