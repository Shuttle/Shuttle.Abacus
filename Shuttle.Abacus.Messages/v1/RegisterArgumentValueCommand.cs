using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterArgumentValueCommand
    {
        public Guid ArgumentId { get; set; }
        public string Value { get; set; }
    }
}