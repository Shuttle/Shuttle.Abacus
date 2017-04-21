using System;

namespace Shuttle.Abacus
{
    public class DeleteArgumentCommand : IDeleteArgumentCommand
    {
        public Guid ArgumentId { get; set; }
    }
}
