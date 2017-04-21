using System;

namespace Shuttle.Abacus
{
    public interface IDeleteArgumentCommand
    {
        Guid ArgumentId { get; set; }
    }
}
