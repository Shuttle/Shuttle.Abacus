using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Shuttle.Abacus.Domain
{
    public class TextPremiumCalculationLogger : IPremiumCalculationLogger
    {
        private readonly string[] argsEmpty = new string[] { };

        private readonly List<string> errorMessages;
        private readonly List<string> informationMessages;
        private readonly StringBuilder log = new StringBuilder();
        private readonly List<string> warningMessages;

        private int indent;

        public TextPremiumCalculationLogger()
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

        public IEnumerable<string> ErrorMessages
        {
            get { return new ReadOnlyCollection<string>(errorMessages); }
        }

        public IEnumerable<string> WarningMessages
        {
            get { return new ReadOnlyCollection<string>(warningMessages); }
        }

        public IEnumerable<string> InformationMessages
        {
            get { return new ReadOnlyCollection<string>(informationMessages); }
        }

        public bool HasErrorMessages
        {
            get { return errorMessages.Count > 0; }
        }

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
