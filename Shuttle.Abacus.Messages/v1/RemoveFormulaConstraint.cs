namespace Shuttle.Abacus.Messages.v1;

public class RemoveFormulaConstraint
{
    public Guid FormulaId { get; set; }
    public Guid ConstraintId { get; set; }
}
