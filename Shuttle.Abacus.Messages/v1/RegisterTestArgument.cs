namespace Shuttle.Abacus.Messages.v1;

public class RegisterTestArgument
{
    public Guid TestId { get; set; }
    public Guid ArgumentId { get; set; }
    public string Value { get; set; } = string.Empty;
}
