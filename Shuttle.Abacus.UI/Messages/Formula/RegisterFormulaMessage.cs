using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Formula
{
    public class RegisterFormulaMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Formula;
    }
}
