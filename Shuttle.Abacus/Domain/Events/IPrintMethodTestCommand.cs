using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IPrintMethodTestCommand
    {
        Guid WorkItemId { get; }
        List<Guid> MethodTestIds { get; }
    }
}
