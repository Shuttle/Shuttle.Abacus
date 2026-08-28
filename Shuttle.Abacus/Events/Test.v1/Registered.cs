namespace Shuttle.Abacus.Events.Test.v1;

public class Registered
{
    public string Name { get; set; } = string.Empty;
    public string ExpectedResult { get; set; } = string.Empty;
    public string ExpectedResultDataTypeName { get; set; } = string.Empty;
    public string Comparison { get; set; } = string.Empty;
    public Guid FormulaId { get; set; }
}
