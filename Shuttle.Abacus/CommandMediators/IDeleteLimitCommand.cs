using System;

namespace Abacus.CommandMediators
{
    public interface IDeleteLimitCommand
    {
        Guid LimitId { get; set; }
    }
}
