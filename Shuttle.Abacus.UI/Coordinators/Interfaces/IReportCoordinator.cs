using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.Report;
using Shuttle.Abacus.UI.Messages.TestCase;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface IReportCoordinator :
        ICoordinator,
        IMessageHandler<MatrixReportMessage>,
        IMessageHandler<TestPrintMessage>
    {

    }
}
