using System;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public class TestRunEvent 
    {
        //TODO
        //public MethodContextDTO FormulaContext { get; set; }
        public Guid WorkItemId { get; set; }
        public Guid MethodTestId { get; set; }
        public string MethodTestDescription { get; set; }
        public string ExpectedResult { get; set; }
    }
}
