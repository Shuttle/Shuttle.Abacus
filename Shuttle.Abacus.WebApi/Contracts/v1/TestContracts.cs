namespace Shuttle.Abacus.WebApi.Contracts.v1;

public class Test
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid AlgorithmId { get; set; }
    public string ExpectedResult { get; set; } = string.Empty;
    public string ExpectedResultDataTypeName { get; set; } = string.Empty;
    public string Comparison { get; set; } = string.Empty;

    public class Specification
    {
        public string NameMatch { get; set; } = string.Empty;
        public List<Guid> Ids { get; set; } = [];
    }
}

public class TestArgument
{
    public Guid ArgumentId { get; set; }
    public string Value { get; set; } = string.Empty;
}

public class TestRunResult
{
    public bool Passed { get; set; }
    public string? Exception { get; set; }
    public decimal Result { get; set; }
    public List<TestRunLogLine> LogLines { get; set; } = [];
    public List<TestRunAlgorithmResult> Results { get; set; } = [];
}

public class TestRunLogLine
{
    public int Indent { get; set; }
    public string Text { get; set; } = string.Empty;
}

public class TestRunAlgorithmResult
{
    public string AlgorithmName { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public int Depth { get; set; }
}
