using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterTestArgumentValueCommand
    {
        public Guid TestId { get; set; }
        public string ArgumentName { get; set; }
        public string Value { get; set; }
    }
}