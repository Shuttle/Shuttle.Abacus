namespace Shuttle.Abacus.Messages.v1;

public class TestExecuted
{
    public Guid Id { get; set; }
    public Guid AlgorithmId { get; set; }
    public decimal Result { get; set; }
    public string Log { get; set; } = string.Empty;
    public TransferObjects.AlgorithmContext? AlgorithmContext { get; set; }
    public string? Exception { get; set; }
}
