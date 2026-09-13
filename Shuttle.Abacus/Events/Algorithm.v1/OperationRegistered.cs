namespace Shuttle.Abacus.Events.Algorithm.v1;

public class OperationRegistered
{
    public Guid Id { get; set; }
    public int SequenceNumber { get; set; }
    public string Operation { get; set; } = string.Empty;
    public string ValueProviderName { get; set; } = string.Empty;
    public string InputParameter { get; set; } = string.Empty;
}
