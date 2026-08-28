namespace Shuttle.Abacus.Messages.v1;

public class RenameFormula
{
    public Guid FormulaId { get; set; }
    public string Name { get; set; } = string.Empty;
}
