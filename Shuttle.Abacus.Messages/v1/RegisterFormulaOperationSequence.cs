// Dead code kept for reference: no participant, handler, or WebApi endpoint sends or handles this message today.
namespace Shuttle.Abacus.Messages.v1;

public class RegisterFormulaOperationSequence
{
    public Guid FormulaId { get; set; }
    public Guid OperationId { get; set; }
    public int SequenceNumber { get; set; }
}
