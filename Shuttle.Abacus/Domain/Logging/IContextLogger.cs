namespace Shuttle.Abacus.Domain
{
    public interface IContextLogger
    {
        ContextLogLevel LogLevel { get; }

        bool IsNormalEnabled { get; }
        bool IsVerboseEnabled { get; }

        void LogNormal(string message);
        void LogVerbose(string message);

        void IncreaseIndent();
        void DecreaseIndent();
    }
}
