using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.SystemUser.Permissions
{
    public class PermissionsPresenter : Presenter<IPermissionsView, IEnumerable<DataRow>>, IPermissionsPresenter
    {
        public PermissionsPresenter(IPermissionsView view)
            : base(view)
        {
            Text = "Permission Details";
            Image = Resources.Image_Permissions;
        }

        public override void OnInitialize()
        {
            View.AssignedPermissions = AssignedPermissions();
            View.AvailablePermissions = Permissions.All().Remove(View.AssignedPermissions);

            View.ValidateView();
        }

        private PermissionCollection AssignedPermissions()
        {
            var result = new PermissionCollection();

            if (Model != null)
            {
                foreach (DataRow row in Model.Table.Rows)
                {
                    result.Add(new Permission(PermissionColumns.Permission.MapFrom(row)));
                }

                result.AssignDescriptionsUsing(Permissions.All());
            }

            return result;
        }
    }
}
