using System;
using Abacus.Validation;

namespace Abacus.UI
{
    public partial class SystemUserView : GenericSystemUserView, ISystemUserView
    {
        public SystemUserView()
        {
            InitializeComponent();
        }

        public string LoginNameValue
        {
            get { return LoginName.Text; }
            set { LoginName.Text = value; }
        }

        public IRuleCollection<object> LoginNameValueRules
        {
            set { ViewValidator.Control(LoginName).ShouldSatisfy(value); }
        }

        private void LoginName_Leave(object sender, EventArgs e)
        {
            Presenter.LoginNameExited();
        }
    }

    public class GenericSystemUserView : View<ISystemUserPresenter>
    {
    }
}
