using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterFormulaOperationCommand
    {
        public Guid FormulaId { get; set; }
        public Guid Id { get; set; }
        public string Operation { get; set; }
        public string ValueProviderName { get; set; }
        public string InputParameter { get; set; }
    }
}