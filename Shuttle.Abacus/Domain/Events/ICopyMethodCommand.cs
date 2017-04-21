using System;

namespace Shuttle.Abacus
{
    public interface ICopyMethodCommand
    {
        Guid MethodId { get; set; }
        string MethodName { get; set; }
    }
}
