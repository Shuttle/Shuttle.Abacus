using System.Collections.Generic;
using NServiceBus;

namespace Abacus.Messages
{
    public class MethodTestPrintEvent : IMessage
    {
        public MethodTestPrintEvent()
        {
            RunEvents = new List<MethodTestRunEvent>();
        }

        public List<MethodTestRunEvent> RunEvents { get; set; }
    }
}
