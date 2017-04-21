using System;

namespace Abacus.CommandMediators
{
    public interface IDeleteFormulaCommand
    {
        Guid FormulaId { get; set; }
    }
}
