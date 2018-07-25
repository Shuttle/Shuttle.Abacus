using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class SetTestArgumentCommand
    {
        public Guid TestId { get; set; }
        public Guid ArgumentId { get; set; }
        public string Value { get; set; }
    }
}