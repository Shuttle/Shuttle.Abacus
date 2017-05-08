using Shuttle.Abacus.Domain;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class TestPrintMessage : NullPermissionMessage
    {
        public TestPrintMessage(MethodTestPrintEvent message)
        {
            Event = message;
        }

        public MethodTestPrintEvent Event { get; set; }
    }
}
