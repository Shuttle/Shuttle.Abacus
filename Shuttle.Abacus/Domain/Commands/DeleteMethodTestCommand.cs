using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class DeleteMethodTestCommand 
    {
        public DeleteMethodTestCommand()
        {
            MethodTestIds = new List<Guid>();
        }

        public List<Guid> MethodTestIds { get; private set; }
    }
}
