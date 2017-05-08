using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class MethodTestPrintEvent 
    {
        public MethodTestPrintEvent()
        {
            RunEvents = new List<TestRunEvent>();
        }

        public List<TestRunEvent> RunEvents { get; set; }
    }
}
