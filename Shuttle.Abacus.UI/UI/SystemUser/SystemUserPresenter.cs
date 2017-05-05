using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.SystemUser;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.UI.SystemUser
{
    public class SystemUserPresenter :
        Presenter<ISystemUserView>,
        ISystemUserPresenter
    {
        private readonly ISystemUserRules _systemUserRules;

        public SystemUserPresenter(ISystemUserView view, ISystemUserRules systemUserRules)
            : base(view)
        {
            Guard.AgainstNull(systemUserRules, "systemUserRules");

            _systemUserRules = systemUserRules;

            Text = "System User Details";
            Image = Resources.Image_SystemUser;
        }

        public void HandleMessage(EditLoginNameMessage message)
        {
            View.LoginNameValue = SystemUserColumns.LoginName.MapFrom(Model.GetRow());
        }

        public void LoginNameExited()
        {
            WorkItem.Text = string.Format("User{0}",
                View.LoginNameValue.Length > 0 ? " : " + View.LoginNameValue : string.Empty);
        }

        public override void OnViewReady()
        {
            base.OnViewReady();

            View.LoginNameValueRules = _systemUserRules.LoginNameRules();
        }
    }
}