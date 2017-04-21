using System;
using System.Collections.Generic;

namespace Abacus.CommandMediators
{
    public interface IDeleteMethodTestCommand
    {
        List<Guid> MethodTestIds { get; }
    }
}
