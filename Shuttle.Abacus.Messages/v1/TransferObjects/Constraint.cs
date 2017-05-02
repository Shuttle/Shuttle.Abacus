using System;

namespace Shuttle.Abacus.Messages.v1.TransferObjects
{
    public class Constraint
    {
        public Guid ArgumentId { get; set; }
        public string Name { get; set; }
        public string Answer { get; set; }
    }
}