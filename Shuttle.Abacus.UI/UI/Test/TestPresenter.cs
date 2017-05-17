using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.UI.Test
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