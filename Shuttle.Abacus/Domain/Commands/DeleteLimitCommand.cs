using System;

namespace Shuttle.Abacus.Domain
{
    public class DeleteLimitCommand 
    {
        public Guid LimitId { get; set; }
    }
}
