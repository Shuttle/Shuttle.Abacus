using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class ChangeLimitCommand 
    {
        public Guid LimitId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
