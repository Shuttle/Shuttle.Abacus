namespace Shuttle.Abacus.WebApi.Contracts.v1;

public class Argument
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DataTypeName { get; set; } = string.Empty;

    public class Specification
    {
        public string NameMatch { get; set; } = string.Empty;
        public List<Guid> Ids { get; set; } = [];
    }
}

public class ArgumentValue
{
    public string Value { get; set; } = string.Empty;
}

public class RenameArgument
{
    public string Name { get; set; } = string.Empty;
}
