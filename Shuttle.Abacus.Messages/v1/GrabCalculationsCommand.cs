using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1
{
    public class GrabCalculationsCommand 
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
