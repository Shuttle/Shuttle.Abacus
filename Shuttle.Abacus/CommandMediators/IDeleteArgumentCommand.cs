using System;

namespace Abacus.CommandMediators
{
    public interface IDeleteArgumentCommand
    {
        Guid ArgumentId { get; set; }
    }
}
