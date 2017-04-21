using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using Abacus.DTO;
using NServiceBus;

namespace Abacus.Messages
{
    public class ChangeFormulaCommand : IMessage, IChangeFormulaCommand
    {
        public Guid CalculationId { get; set; }
        public Guid FormulaId { get; set; }
        public List<OperationDTO> Operations { get; set; }
        public List<ConstraintDTO> Constraints { get; set; }
    }
}
