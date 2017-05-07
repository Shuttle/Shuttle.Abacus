using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Formula
{
    public class NewFormulaMessage : Message
    {
        public NewFormulaMessage()
        {
        }

        public NewFormulaMessage(NewFormulaFromExistingMessage message)
            : this()
        {
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Formula; }
        }
    }
}
