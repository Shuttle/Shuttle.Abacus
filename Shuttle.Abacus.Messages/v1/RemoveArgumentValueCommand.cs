using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RemoveArgumentValueCommand 
    {
        public Guid ArgumentId { get; set; }
        public string Value { get; set; }
    }
}
