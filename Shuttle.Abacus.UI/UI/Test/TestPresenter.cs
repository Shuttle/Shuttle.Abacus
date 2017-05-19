using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.Test
{
    public class TestPresenter : Presenter<ITestView, TestModel>, ITestPresenter
    {
        public TestPresenter(ITestView view) : base(view)
        {
            Text = "Test Details";

            Image = Resources.Image_Test;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            View.PopulateFormulas(Model.Formulas);

            View.NameValue = Model.Name;
            View.ExpectedResultValue = Model.ExpectedResult;
            View.ExpectedResultTypeValue = Model.ExpectedResultType;
            View.ComparisonValue = Model.Comparison;
        }
    }
}