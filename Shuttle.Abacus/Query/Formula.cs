namespace Shuttle.Abacus.Query;

public class Formula
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? MaximumFormulaName { get; set; }
    public string? MinimumFormulaName { get; set; }

    public class Operation
    {
        public Guid Id { get; set; }
        public Guid FormulaId { get; set; }
        public int SequenceNumber { get; set; }
        public string OperationName { get; set; } = string.Empty;
        public string ValueProviderName { get; set; } = string.Empty;
        public string InputParameter { get; set; } = string.Empty;
    }

    public class Constraint
    {
        public Guid Id { get; set; }
        public Guid FormulaId { get; set; }
        public Guid ArgumentId { get; set; }
        public string Comparison { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class Specification : Specification<Specification>
    {
        public string NameMatch { get; private set; } = string.Empty;

        public Specification WithNameMatch(string nameMatch)
        {
            NameMatch = nameMatch;

            return this;
        }
    }
}
