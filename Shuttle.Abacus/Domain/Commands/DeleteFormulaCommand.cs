using System;

namespace Shuttle.Abacus
{
    public class DeleteFormulaCommand : IDeleteFormulaCommand
    {
        public Guid FormulaId { get; set; }
    }
}
