using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.UI.Test.RunTest
{
    public class RunTestPresenter : Presenter<IRunTestView, RunTestModel>, IRunTestPresenter
    {
        private readonly IExecutionTask _executionTask;

        public RunTestPresenter(IRunTestView view, IExecutionTask executionTask) : base(view)
        {
            Guard.AgainstNull(view, "view");
            Guard.AgainstNull(executionTask, "executionTask");

            Text = "Run Test Details";

            _executionTask = executionTask;

            Image = Resources.Image_Test;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            _executionTask.Execute(Model.FormulaName, Model.Arguments);
            _executionTask.Flush();
        }
    }
}