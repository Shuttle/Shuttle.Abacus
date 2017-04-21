using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IGrabCalculationsCommand
    {
        Guid GrabberCalculationId { get; set; }
        Guid MethodId { get; set; }
        List<Guid> GrabbedCalculationIds { get; set; }
    }
}
