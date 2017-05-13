using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.ArgumentValue
{
    public partial class ArgumentValueView : GenericArgumentValueView, IArgumentValueView
    {
        public ArgumentValueView()
        {
            InitializeComponent();
        }

        public string ValueValue
        {
            get { return Value.Text; }
            set { Value.Text = value; }
        }

        public IRuleCollection<object> ValueRules
        {
            set
            {
                ViewValidator.Control(Value).ShouldSatisfy(value);
            }
        }
    }

    public class GenericArgumentValueView : View<IArgumentValuePresenter>
    {
    }
}
