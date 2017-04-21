using System;
using System.Collections.Generic;

namespace Abacus.CommandMediators
{
    public interface IRunMethodTestCommand
    {
        Guid WorkItemId { get; }
        List<Guid> MethodTestIds { get; }
    }
}
