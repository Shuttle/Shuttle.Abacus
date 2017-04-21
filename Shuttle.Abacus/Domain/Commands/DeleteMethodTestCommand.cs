using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class DeleteMethodTestCommand : IDeleteMethodTestCommand
    {
        public DeleteMethodTestCommand()
        {
            MethodTestIds = new List<Guid>();
        }

        public List<Guid> MethodTestIds { get; private set; }
    }
}
