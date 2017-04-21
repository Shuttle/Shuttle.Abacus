using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface IPremiumCalculationLogger
    {
        bool Enabled { get; }

        void AppendLine(string text);
        void AppendLine(string text, params string[] args);
        void AppendLine ();

        IEnumerable<string> ErrorMessages { get; }
        IEnumerable<string> WarningMessages { get; }
        IEnumerable<string> InformationMessages { get; }

        bool HasErrorMessages { get; }

        void AddErrorMessage(string message);
        void AddWarningMessage(string message);
        void AddInformationMessage(string message);

        void IncreaseIndent();
        void DecreaseIndent();
    }
}
