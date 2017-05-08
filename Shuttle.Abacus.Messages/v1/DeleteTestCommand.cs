using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1
{
    public class DeleteTestCommand 
    {
        public DeleteTestCommand()
        {
            MethodTestIds = new List<Guid>();
        }

        public List<Guid> MethodTestIds { get; private set; }
    }
}
