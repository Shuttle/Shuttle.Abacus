using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.FactorAnswer
{
    public class NewArgumentMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Argument; }
        }
    }
}