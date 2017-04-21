using System;

namespace Shuttle.Abacus
{
    public interface IDeleteLimitCommand
    {
        Guid LimitId { get; set; }
    }
}
