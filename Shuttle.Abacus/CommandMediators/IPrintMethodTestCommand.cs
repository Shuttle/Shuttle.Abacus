using System;
using System.Collections.Generic;

namespace Abacus.CommandMediators
{
    public interface IPrintMethodTestCommand
    {
        Guid WorkItemId { get; }
        List<Guid> MethodTestIds { get; }
    }
}
