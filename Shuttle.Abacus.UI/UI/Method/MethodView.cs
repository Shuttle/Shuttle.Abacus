using System;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Method
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
