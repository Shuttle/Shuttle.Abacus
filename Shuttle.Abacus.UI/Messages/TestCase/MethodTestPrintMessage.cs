using Shuttle.Abacus.Domain;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class MethodTestPrintMessage : NullPermissionMessage
    {
        public MethodTestPrintMessage(MethodTestPrintEvent message)
        {
            Event = message;
        }

        public MethodTestPrintEvent Event { get; set; }
    }
}
