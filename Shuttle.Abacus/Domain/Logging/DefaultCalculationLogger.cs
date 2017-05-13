using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Shuttle.Abacus.Domain
{
    public class DefaultCalculationLogger : ICalculationLogger
    {
        private readonly string[] argsEmpty = { };

        private readonly List<string> errorMessages;
        private readonly List<string> informationMessages;
        private readonly StringBuilder log = new StringBuilder();
        private readonly List<string> warningMessages;

        private int indent;

        public DefaultCalculationLogger()
        {
            Enabled = true;

            errorMessages = new List<string>();
            warningMessages = new List<string>();
            informationMessages = new List<string>();

            indent = 0;
        }

        public virtual bool Enabled { get; private set; }

        public virtual void AppendLine(string text)
        {
            AppendLine(text, argsEmpty);
        }

        public virtual void AppendLine(string text, params string[] args)
        {
            log.AppendFormat(new string('\t', indent) + text, args);
            log.AppendLine();
        }

        public virtual void AppendLine()
        {
            log.AppendLine();
        }

        public IEnumerable<string> ErrorMessages => new ReadOnlyCollection<string>(errorMessages);

        public IEnumerable<string> WarningMessages => new ReadOnlyCollection<string>(warningMessages);

        public IEnumerable<string> InformationMessages => new ReadOnlyCollection<string>(informationMessages);

        public bool HasErrorMessages => errorMessages.Count > 0;

        public void AddErrorMessage(string message)
        {
            if (errorMessages.Contains(message))
            {
                return;
            }

            errorMessages.Add(message);

            if (Enabled)
            {
                AppendLine(string.Format("ERROR - {0}", message));
            }
        }

        public void AddWarningMessage(string message)
        {
            if (warningMessages.Contains(message))
            {
                return;
            }

            warningMessages.Add(message);

            if (Enabled)
            {
                AppendLine(string.Format("WARNING - {0}", message));
            }
        }

        public void AddInformationMessage(string message)
        {
            if (informationMessages.Contains(message))
            {
                return;
            }

            informationMessages.Add(message);

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
            return log.ToString();
        }
    }
}
