using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class DeleteMethodTestCommand : IMessage, IDeleteMethodTestCommand
    {
        public DeleteMethodTestCommand()
        {
            MethodTestIds = new List<Guid>();
        }

        public List<Guid> MethodTestIds { get; private set; }
    }
}
