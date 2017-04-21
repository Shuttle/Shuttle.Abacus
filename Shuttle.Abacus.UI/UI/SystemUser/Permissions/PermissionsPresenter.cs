using System.Data;
using Abacus.Data;
using Abacus.Infrastructure;
using Abacus.Localisation;

namespace Abacus.UI
{
    public class PermissionsPresenter : Presenter<IPermissionsView, IQueryResult>, IPermissionsPresenter
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
