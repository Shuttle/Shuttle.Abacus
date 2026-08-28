// Dead code kept for reference: the corresponding participant throws NotImplementedException — this feature was
// never finished.
namespace Shuttle.Abacus.Messages.v1;

public class RegisterFormulaMaximum
{
    public Guid FormulaId { get; set; }
    public string Name { get; set; } = string.Empty;
}
