using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.UI.Test.RunTest
{
    public class RunTestPresenter : Presenter<IRunTestView, RunTestModel>, IRunTestPresenter
    {
        private readonly ICalculationService _calculationService;

        public RunTestPresenter(IRunTestView view, ICalculationService calculationService) : base(view)
        {
            Guard.AgainstNull(view, "view");
            Guard.AgainstNull(calculationService, "calculationService");

            Text = "Run Test Details";

            _calculationService = calculationService;

            Image = Resources.Image_Test;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            _calculationService.Execute(Model.FormulaName, Model.Arguments);
        }
    }
}