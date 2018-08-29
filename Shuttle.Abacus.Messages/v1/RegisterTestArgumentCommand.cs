using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterTestArgumentCommand
    {
        public Guid TestId { get; set; }
        public Guid ArgumentId { get; set; }
        public string Value { get; set; }
    }
}