using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;

namespace Shuttle.Abacus.UI.WorkItemControllers
{
    public class ReportController : WorkItemController, IReportController
    {
        private readonly IApplicationConfiguration _applicationConfiguration;

        public ReportController(IApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }

    }
}
