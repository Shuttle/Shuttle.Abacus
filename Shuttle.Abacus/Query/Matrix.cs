namespace Shuttle.Abacus.Query;

public class Matrix
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid RowArgumentId { get; set; }
    public Guid? ColumnArgumentId { get; set; }
    public string DataTypeName { get; set; } = string.Empty;

    public class Constraint
    {
        public Guid Id { get; set; }
        public Guid MatrixId { get; set; }
        public string Axis { get; set; } = string.Empty;
        public int Index { get; set; }
        public string Comparison { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class Element
    {
        public Guid Id { get; set; }
        public Guid MatrixId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
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
