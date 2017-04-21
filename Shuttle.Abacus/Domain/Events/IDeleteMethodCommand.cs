using System;

namespace Shuttle.Abacus
{
    public interface IDeleteMethodCommand
    {
        Guid MethodId { get; set; }
    }
}
