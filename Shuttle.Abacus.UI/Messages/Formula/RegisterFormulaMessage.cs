using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Formula
{
    public class RegisterFormulaMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Formula;
    }
}
