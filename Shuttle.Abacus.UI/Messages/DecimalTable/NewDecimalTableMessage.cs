using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.DecimalTable
{
    public class NewDecimalTableMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.DecimalTable; }
        }
    }
}
