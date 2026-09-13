namespace Shuttle.Abacus.Messages.v1;

public class AlgorithmOperationRegistered
{
    public Guid Id { get; set; }
    public Guid AlgorithmId { get; set; }
    public int SequenceNumber { get; set; }
    public string Operation { get; set; } = string.Empty;
    public string ValueProviderName { get; set; } = string.Empty;
    public string InputParameter { get; set; } = string.Empty;
}
