using System;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public class MethodTestRunEvent 
    {
        //TODO
        //public MethodContextDTO MethodContext { get; set; }
        public Guid WorkItemId { get; set; }
        public Guid MethodTestId { get; set; }
        public string MethodTestDescription { get; set; }
        public decimal ExpectedResult { get; set; }
    }
}
