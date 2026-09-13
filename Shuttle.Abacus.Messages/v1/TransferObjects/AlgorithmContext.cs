namespace Shuttle.Abacus.Messages.v1.TransferObjects;

public class AlgorithmContext
{
    public decimal Result { get; set; }
    public DateTime DateStarted { get; set; }
    public DateTime DateCompleted { get; set; }
    public List<ArgumentValue> ArgumentAnswers { get; set; } = [];
    public List<AlgorithmContext> AlgorithmContexts { get; set; } = [];
}
