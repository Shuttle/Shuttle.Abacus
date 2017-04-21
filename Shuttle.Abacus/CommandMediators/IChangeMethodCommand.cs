using System;

namespace Shuttle.Abacus
{
    public interface IChangeMethodCommand
    {
        Guid MethodId { get; set; }
        string MethodName { get; set; }
    }
}
