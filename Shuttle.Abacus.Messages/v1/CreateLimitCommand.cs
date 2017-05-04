using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class CreateLimitCommand
    {
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
