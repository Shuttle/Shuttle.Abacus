using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IContextLogger
    {
        ContextLogLevel LogLevel { get; }

        bool IsNormalEnabled { get; }
        bool IsVerboseEnabled { get; }
        IEnumerable<ContextLogLine> Lines { get; }

        void LogNormal(string message);
        void LogVerbose(string message);

        void IncreaseIndent();
        void DecreaseIndent();
    }
}