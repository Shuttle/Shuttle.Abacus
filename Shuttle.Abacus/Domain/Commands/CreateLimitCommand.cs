using System;

namespace Shuttle.Abacus
{
    public class CreateLimitCommand : ICreateLimitCommand
    {
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
