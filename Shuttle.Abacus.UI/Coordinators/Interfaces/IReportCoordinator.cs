using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Messages.Report;
using Shuttle.Abacus.Shell.Messages.Test;

namespace Shuttle.Abacus.Shell.Coordinators.Interfaces
{
    public interface IReportCoordinator :
        ICoordinator,
        IMessageHandler<MatrixReportMessage>,
        IMessageHandler<TestPrintMessage>
    {

    }
}
