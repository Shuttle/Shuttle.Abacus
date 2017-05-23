using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Shuttle.Abacus.Domain
{
    public class ExecutionContextLogger : IExecutionContextLogger
    {
        private readonly string[] argsEmpty = { };

        private readonly StringBuilder _log = new StringBuilder();

        private readonly List<string> _errorMessages;
        private readonly List<string> _informationMessages;
        private readonly List<string> _warningMessages;

        private int indent;

        public ExecutionContextLogger()
        {
            Enabled = true;

            _errorMessages = new List<string>();
            _warningMessages = new List<string>();
            _informationMessages = new List<string>();

            indent = 0;
        }

        public virtual bool Enabled { get; private set; }

        public virtual void AppendLine(string text)
        {
            AppendLine(text, argsEmpty);
        }

        public virtual void AppendLine(string text, params string[] args)
        {
            _log.AppendFormat(new string('\t', indent) + text, args);
            _log.AppendLine();
        }

        public virtual void AppendLine()
        {
            _log.AppendLine();
        }

        public IEnumerable<string> ErrorMessages => new ReadOnlyCollection<string>(_errorMessages);

        public IEnumerable<string> WarningMessages => new ReadOnlyCollection<string>(_warningMessages);

        public IEnumerable<string> InformationMessages => new ReadOnlyCollection<string>(_informationMessages);

        public bool HasErrorMessages => _errorMessages.Count > 0;

        public void AddErrorMessage(string message)
        {
            if (_errorMessages.Contains(message))
            {
                return;
            }

            _errorMessages.Add(message);

            if (Enabled)
            {
                AppendLine(string.Format("ERROR - {0}", message));
            }
        }

        public void AddWarningMessage(string message)
        {
            if (_warningMessages.Contains(message))
            {
                return;
            }

            _warningMessages.Add(message);

            if (Enabled)
            {
                AppendLine(string.Format("WARNING - {0}", message));
            }
        }

        public void AddInformationMessage(string message)
        {
            if (_informationMessages.Contains(message))
            {
                return;
            }

            _informationMessages.Add(message);

            if (Enabled)
            {
                AppendLine(string.Format("INFO - {0}", message));
            }
        }

        public void IncreaseIndent()
        {
            indent++;
        }

        public void DecreaseIndent()
        {
            indent--;

            if (indent < 0)
            {
                indent = 0;
            }
        }

        public override string ToString()
        {
            return _log.ToString();
        }
    }
}
