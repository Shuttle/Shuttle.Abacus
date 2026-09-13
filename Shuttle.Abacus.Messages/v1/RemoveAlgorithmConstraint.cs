namespace Shuttle.Abacus.Messages.v1;

public class RemoveAlgorithmConstraint
{
    public Guid AlgorithmId { get; set; }
    public Guid ConstraintId { get; set; }
}
