using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class GrabCalculationsCommand : IGrabCalculationsCommand
    {
        public GrabCalculationsCommand()
        {
            GrabbedCalculationIds = new List<Guid>();
        }

        public Guid GrabberCalculationId { get; set; }
        public Guid MethodId { get; set; }
        public List<Guid> GrabbedCalculationIds { get; set; }        
    }
}
