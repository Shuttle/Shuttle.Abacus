using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterFormulaCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}