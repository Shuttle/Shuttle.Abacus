using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using Abacus.DTO;
using NServiceBus;

namespace Abacus.Messages
{
    public class CreateFormulaCommand : IMessage, ICreateFormulaCommand
    {
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }
        public List<OperationDTO> Operations { get; set; }
        public List<ConstraintDTO> Constraints { get; set; }
    }
}
