namespace Shuttle.Abacus.WebApi.Contracts.v1;

public class Algorithm
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public class Specification
    {
        public string NameMatch { get; set; } = string.Empty;
        public List<Guid> Ids { get; set; } = [];
    }
}

public class RenameAlgorithm
{
    public string Name { get; set; } = string.Empty;
}

public class AlgorithmOperation
{
    public Guid? Id { get; set; }
    public string Operation { get; set; } = string.Empty;
    public string ValueProviderName { get; set; } = string.Empty;
    public string InputParameter { get; set; } = string.Empty;
}

public class AlgorithmConstraint
{
    public Guid? Id { get; set; }
    public Guid ArgumentId { get; set; }
    public string Comparison { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
