using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Test
{
    public partial class TestView : GenericTestView, ITestView
    {
        public TestView()
        {
            InitializeComponent();
        }

        public string NameValue
        {
            get { return Description.Text; }
            set { Description.Text = value; }
        }

        public string ExpectedResultValue
        {
            get
            {
                return ExpectedResult.Text;
            }
            set { ExpectedResult.Text = value; }
        }

        public IRuleCollection<object> NameRules
        {
            set { ViewValidator.Control(Description).ShouldSatisfy(value); }
        }

        public IRuleCollection<object> ExpectedResultRules
        {
            set { ViewValidator.Control(ExpectedResult).ShouldSatisfy(value); }
        }

        public string ExpectedResultTypeValue { get; set; }
        public string ComparisonValue { get; set; }
    }

    public class GenericTestView : View<ITestPresenter>
    {
    }
}