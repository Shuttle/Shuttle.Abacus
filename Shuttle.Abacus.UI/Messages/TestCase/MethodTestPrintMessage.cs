using Abacus.Messages;

namespace Abacus.UI
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
