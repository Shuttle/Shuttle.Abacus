using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.SystemUser;

namespace Shuttle.Abacus.UI.UI.SystemUser
{
    public class SystemUserPresenter :
        Presenter<ISystemUserView>,
        ISystemUserPresenter
    {
        private readonly ISystemUserRules systemUserRules;

        public SystemUserPresenter(ISystemUserView view, ISystemUserRules systemUserRules)
            : base(view)
        {
            this.systemUserRules = systemUserRules;

            Text = "System User Details";
            Image = Resources.Image_SystemUser;
        }

        public void HandleMessage(EditLoginNameMessage message)
        {
            var row = Model.Row;

            View.LoginNameValue = SystemUserColumns.LoginName.MapFrom(row);
        }

        public void LoginNameExited()
        {
            WorkItem.Text = string.Format("User{0}",
                                          View.LoginNameValue.Length > 0 ? " : " + View.LoginNameValue : string.Empty);
        }

        public override void OnViewReady()
        {
            base.OnViewReady();

            View.LoginNameValueRules = systemUserRules.LoginNameRules();
        }
    }
}
