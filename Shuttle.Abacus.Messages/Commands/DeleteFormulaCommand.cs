using System;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class DeleteFormulaCommand : IMessage, IDeleteFormulaCommand
    {
        public Guid FormulaId { get; set; }
    }
}
