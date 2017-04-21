using System;

namespace Shuttle.Abacus
{
    public class DeleteMethodCommand : IDeleteMethodCommand
    {
        public Guid MethodId { get; set; }
    }
}
