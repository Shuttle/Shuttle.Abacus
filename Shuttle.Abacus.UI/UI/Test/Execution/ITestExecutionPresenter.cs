using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.Test.Execution
{
    public interface ITestExecutionPresenter : IPresenter
    {
        void ProcessModel(TestExecutionModel model);
        void RequestExecution(TestExecutionItemModel item);
    }
}
