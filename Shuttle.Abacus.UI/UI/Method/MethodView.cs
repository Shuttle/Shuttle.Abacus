using System;
using Abacus.Validation;

namespace Abacus.UI
{
    public partial class MethodView : GenericMethodView, IMethodView
    {
        public MethodView()
        {
            InitializeComponent();
        }

        public string MethodNameValue
        {
            get { return Title.Text; }
            set { Title.Text = value; }
        }

        public IRuleCollection<object> MethodNameRules
        {
            set { ViewValidator.Control(Title).ShouldSatisfy(value); }
        }

        private void Title_Leave(object sender, EventArgs e)
        {
            Presenter.TitleExited();
        }
    }

    public class GenericMethodView : View<IMethodPresenter>
    {
    }
}
