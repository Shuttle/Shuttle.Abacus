using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Messages.Test;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.Test.Execution
{
    public interface ITestExecutionView : IView
    {
        void AddTest(TestExecutionItemModel item);
        void TestExecuted(TestExecutedMessage message);
    }
}