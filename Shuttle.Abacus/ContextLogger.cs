using System.Collections.Generic;
using System.Text;

namespace Shuttle.Abacus
{
    public enum ContextLogLevel
    {
        None = 0,
        Normal = 1,
        Verbose = 2
    }

    public class ContextLogger : IContextLogger
    {
        private readonly object[] _argsEmpty = { };

        private readonly List<ContextLogLine> _lines = new List<ContextLogLine>();

        private int _indent;

        public ContextLogger(ContextLogLevel logLevel)
        {
            _indent = 0;

            LogLevel = logLevel;
        }

        public ContextLogLevel LogLevel { get; }

        public bool IsNormalEnabled => LogLevel != ContextLogLevel.None;
        public bool IsVerboseEnabled => LogLevel == ContextLogLevel.Verbose;

        public IEnumerable<ContextLogLine> Lines => _lines.AsReadOnly();

        public void LogNormal(string message)
        {
            if (LogLevel == ContextLogLevel.None)
            {
                return;
            }

            Log(message);
        }

        public void LogVerbose(string message)
        {
            if (LogLevel != ContextLogLevel.Verbose)
            {
                return;
            }

            Log(message);
        }

        public void IncreaseIndent()
        {
            _indent++;
        }

        public void DecreaseIndent()
        {
            _indent--;

            if (_indent < 0)
            {
                _indent = 0;
            }
        }

        private void AppendLine(string text)
        {
            AppendLine(text, _argsEmpty);
        }

        private void AppendLine(string text, params object[] args)
        {
            _lines.Add(new ContextLogLine {Indent = _indent, Text = string.Format(text, args)});
        }

        private void Log(string message)
        {
            AppendLine($"{message}");
        }
    }

    public class ContextLogLine
    {
        public int Indent { get; set; }
        public string Text { get; set; }
    }
}