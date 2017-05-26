using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.UI.Test.Execution
{
    public class TestExecutionPresenter : Presenter<ITestExecutionView, TestExecutionModel>, ITestExecutionPresenter
    {
        public TestExecutionPresenter(ITestExecutionView executionView) : base(executionView)
        {
            Guard.AgainstNull(executionView, "view");
            
            Text = "Run Test Details";

            Image = Resources.Image_Test;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
        }
    }
}