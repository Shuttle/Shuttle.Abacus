namespace Shuttle.Abacus.WebApi.Contracts.v1;

public class Matrix
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid RowArgumentId { get; set; }
    public Guid? ColumnArgumentId { get; set; }
    public string DataTypeName { get; set; } = string.Empty;

    public class Specification
    {
        public string NameMatch { get; set; } = string.Empty;
        public List<Guid> Ids { get; set; } = [];
    }
}

public class MatrixConstraint
{
    public string Axis { get; set; } = string.Empty;
    public int Index { get; set; }
    public string Comparison { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

public class MatrixElement
{
    public int Row { get; set; }
    public int Column { get; set; }
    public string Value { get; set; } = string.Empty;
}
