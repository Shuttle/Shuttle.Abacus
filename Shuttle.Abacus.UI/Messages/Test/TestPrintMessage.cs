using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Shell.Messages.Core;

namespace Shuttle.Abacus.Shell.Messages.Test
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
