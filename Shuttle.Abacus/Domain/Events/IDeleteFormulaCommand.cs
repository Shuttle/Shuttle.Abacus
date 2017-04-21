using System;

namespace Shuttle.Abacus
{
    public interface IDeleteFormulaCommand
    {
        Guid FormulaId { get; set; }
    }
}
