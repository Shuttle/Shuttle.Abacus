using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IRunMethodTestCommand
    {
        Guid WorkItemId { get; }
        List<Guid> MethodTestIds { get; }
    }
}
