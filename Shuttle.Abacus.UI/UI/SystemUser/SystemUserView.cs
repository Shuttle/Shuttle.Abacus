using System;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.SystemUser
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
