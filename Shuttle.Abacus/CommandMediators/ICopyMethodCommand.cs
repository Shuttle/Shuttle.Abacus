using System;

namespace Abacus.CommandMediators
{
    public interface ICopyMethodCommand
    {
        Guid MethodId { get; set; }
        string MethodName { get; set; }
    }
}
