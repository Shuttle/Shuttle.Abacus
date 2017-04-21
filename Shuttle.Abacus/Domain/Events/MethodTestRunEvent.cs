using System;

namespace Shuttle.Abacus
{
    public class MethodTestRunEvent 
    {
        public MethodContextDTO MethodContext { get; set; }
        public Guid WorkItemId { get; set; }
        public Guid MethodTestId { get; set; }
        public string MethodTestDescription { get; set; }
        public decimal ExpectedResult { get; set; }
    }
}
