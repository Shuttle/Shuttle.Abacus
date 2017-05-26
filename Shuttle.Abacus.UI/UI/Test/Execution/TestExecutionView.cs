using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.Test.Execution
{
    public partial class TestExecutionExecutionView : GenericTestExecutionView, ITestExecutionView
    {
        public TestExecutionExecutionView()
        {
            InitializeComponent();
        }
    }

    public class GenericTestExecutionView : View<ITestExecutionPresenter>
    {
    }
}