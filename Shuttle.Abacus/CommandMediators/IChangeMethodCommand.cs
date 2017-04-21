using System;

namespace Abacus.CommandMediators
{
    public interface IChangeMethodCommand
    {
        Guid MethodId { get; set; }
        string MethodName { get; set; }
    }
}
