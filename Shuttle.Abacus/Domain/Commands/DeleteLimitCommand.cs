using System;

namespace Shuttle.Abacus
{
    public class DeleteLimitCommand : IDeleteLimitCommand
    {
        public Guid LimitId { get; set; }
    }
}
