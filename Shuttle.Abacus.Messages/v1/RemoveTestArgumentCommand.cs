using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RemoveTestArgumentCommand
    {
        public Guid TestId { get; set; }
        public string ArgumentName { get; set; }
    }
}