using System;

namespace Abacus.CommandMediators
{
    public interface IDeleteMethodCommand
    {
        Guid MethodId { get; set; }
    }
}
