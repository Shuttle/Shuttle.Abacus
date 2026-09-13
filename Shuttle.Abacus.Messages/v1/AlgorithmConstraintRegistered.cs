namespace Shuttle.Abacus.Messages.v1;

public class AlgorithmConstraintRegistered
{
    public Guid Id { get; set; }
    public Guid AlgorithmId { get; set; }
    public Guid ArgumentId { get; set; }
    public string Comparison { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
