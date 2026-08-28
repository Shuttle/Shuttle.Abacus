using System.Text;

namespace Shuttle.Abacus;

public enum ContextLogLevel
{
    None = 0,
    Normal = 1,
    Verbose = 2
}

public class ContextLogger(ContextLogLevel logLevel) : IContextLogger
{
    private readonly List<ContextLogLine> _lines = [];

    private int _indent;

    public ContextLogLevel LogLevel { get; } = logLevel;

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

    private void Log(string message)
    {
        _lines.Add(new() { Indent = _indent, Text = message });
    }

    public override string ToString()
    {
        var result = new StringBuilder();

        foreach (var line in _lines)
        {
            result.AppendLine($"{new string('\t', line.Indent)}{line.Text}");
        }

        return result.ToString();
    }
}

public class ContextLogLine
{
    public int Indent { get; set; }
    public string Text { get; set; } = string.Empty;
}
