namespace Shuttle.Abacus.Messages.v1;

public class RenameArgument
{
    public Guid ArgumentId { get; set; }
    public string Name { get; set; } = string.Empty;
}
