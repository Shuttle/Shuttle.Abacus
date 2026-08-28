namespace Shuttle.Abacus.Messages.v1;

public class TestRegistered
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid FormulaId { get; set; }
    public string ExpectedResult { get; set; } = string.Empty;
    public string ExpectedResultDataTypeName { get; set; } = string.Empty;
    public string Comparison { get; set; } = string.Empty;
}
