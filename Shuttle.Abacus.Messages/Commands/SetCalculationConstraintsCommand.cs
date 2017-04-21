using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using Abacus.DTO;
using NServiceBus;

namespace Abacus.Messages
{
    public class SetCalculationConstraintsCommand : IMessage, ISetCalculationConstraintsCommand
    {
        public Guid CalculationId { get; set; }
        public List<ConstraintDTO> Constraints { get; set; }
    }
}
