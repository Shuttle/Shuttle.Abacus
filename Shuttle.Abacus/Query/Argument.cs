namespace Shuttle.Abacus.Query;

public class Argument
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DataTypeName { get; set; } = string.Empty;

    public class Value
    {
        public Guid ArgumentId { get; set; }
        public string ArgumentValue { get; set; } = string.Empty;
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
