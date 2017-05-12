using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1
{
    public class RenameArgumentCommand
    {
        public RenameArgumentCommand()
        {
            new List<string>();
        }

        public Guid ArgumentId { get; set; }
        public string Name { get; set; }
    }
}
