using System;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class DeleteCalculationCommand : IMessage, IDeleteCalculationCommand
    {
        public Guid CalculationId { get; set; }
        public Guid MethodId { get; set; }
    }
}
