namespace Shuttle.Abacus.Messages.v1;

public class RegisterFormulaOperation
{
    public Guid Id { get; set; }
    public Guid FormulaId { get; set; }
    public string Operation { get; set; } = string.Empty;
    public string ValueProviderName { get; set; } = string.Empty;
    public string InputParameter { get; set; } = string.Empty;
}
