using System.Windows.Forms;
using Abacus.Infrastructure;
using Abacus.Messages;
using System.IO;

namespace Abacus.UI
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
