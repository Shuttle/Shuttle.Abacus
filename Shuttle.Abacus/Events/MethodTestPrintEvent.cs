using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class MethodTestPrintEvent 
    {
        public MethodTestPrintEvent()
        {
            RunEvents = new List<MethodTestRunEvent>();
        }

        public List<MethodTestRunEvent> RunEvents { get; set; }
    }
}
