namespace Shuttle.Abacus.SqlServer;

public class SqlServerOptions
{
    public const string SectionName = "Shuttle:Abacus:SqlServer";

    public string ConnectionString { get; set; } = string.Empty;
}
