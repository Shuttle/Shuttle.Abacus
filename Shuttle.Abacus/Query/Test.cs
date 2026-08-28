namespace Shuttle.Abacus.Query;

public class Test
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid FormulaId { get; set; }
    public string ExpectedResult { get; set; } = string.Empty;
    public string ExpectedResultDataTypeName { get; set; } = string.Empty;
    public string Comparison { get; set; } = string.Empty;

    public class Argument
    {
        public Guid TestId { get; set; }
        public Guid ArgumentId { get; set; }
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
