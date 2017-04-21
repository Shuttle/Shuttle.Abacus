using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class NewDecimalTableMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.DecimalTable; }
        }
    }
}
