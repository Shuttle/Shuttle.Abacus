using Abacus.Validation;

namespace Abacus.UI
{
    public partial class LimitView : GenericLimitView, ILimitView
    {
        public LimitView()
        {
            InitializeComponent();
        }

        public string LimitNameValue
        {
            get { return LimitName.Text; }
            set { LimitName.Text = value; }
        }

        public string TypeValue
        {
            get { return Type.Text; }
            set { Type.Text = value; }
        }

        public IRuleCollection<object> LimitNameRules
        {
            set { ViewValidator.Control(LimitName).ShouldSatisfy(value); }
        }

        public IRuleCollection<object> TypeRules
        {
            set { ViewValidator.Control(Type).ShouldSatisfy(value); }
        }

        private void LimitName_Leave(object sender, System.EventArgs e)
        {
            Presenter.LimitNameExited();
        }
    }

    public class GenericLimitView : View<ILimitPresenter>
    {
    }
}
