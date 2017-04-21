using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IDeleteMethodTestCommand
    {
        List<Guid> MethodTestIds { get; }
    }
}
