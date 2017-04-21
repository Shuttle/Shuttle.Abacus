using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class GrabCalculationsCommand : IMessage, IGrabCalculationsCommand
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
