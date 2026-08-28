namespace Shuttle.Abacus.Messages.v1;

public class RemoveTestArgument
{
    public Guid TestId { get; set; }
    public Guid ArgumentId { get; set; }
}
