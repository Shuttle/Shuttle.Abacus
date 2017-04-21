using System;

namespace Abacus.CommandMediators
{
    public interface IChangeLimitCommand
    {
        string Name { get; set; }
        string Type { get; set; }
        Guid LimitId { get; set; }
    }
}
