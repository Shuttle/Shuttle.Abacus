using System;

namespace Shuttle.Abacus.WebApi
{
    public class FormulaOperationModel
    {
        public Guid Id { get; set; }
        public Guid FormulaId { get; set; }
        public int SequenceNumber { get; set; }
        public string Operation { get; set; }
        public string ValueProviderName { get; set; }
        public string InputParameter { get; set; }
    }
}