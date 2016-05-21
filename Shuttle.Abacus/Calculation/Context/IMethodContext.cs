using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IMethodContext : ICalculationDisplay<IMethodContext>,
                                       ICanBeReadOnly<IMethodContext>,
                                       IDisposable
    {
        IEnumerable<ICalculationResult> Results { get; }
        IEnumerable<ArgumentAnswer> ArgumentAnswers { get; }
        IEnumerable<string> ErrorMessages { get; }
        IEnumerable<string> WarningMessages { get; }
        IEnumerable<string> InformationMessages { get; }
        ICalculationResult Total { get; }
        bool LoggerEnabled { get; }
        string LogText { get; }
        bool OK { get; }
        IEnumerable<ICalculationResult> SubTotals { get; }
        List<GraphNodeDTO> GraphNodes();
        void AddErrorMessage(string message);
        void AddWarningMessage(string message);
        void AddInformationMessage(string message);
        ICalculationResult GetResult(string name);
        SubTotalCalculationResult GetSubTotal(string name);
        bool HasResult(string name);
        bool HasArgumentAnswer(string argumentName);
        IMethodContext AddArgumentAnswer(ArgumentAnswer argumentAnswer);
        ArgumentAnswer GetArgumentAnswer(string argumentName);
        void Log();
        void Log(string text, params string[] args);
        void IncreaseIndent();
        void DecreaseIndent();
        IMethodContext AddResult(ICalculationResult calculationResult);
        IMethodContext Wrapped(string title);
        IMethodContext Copy();

        void LogResults();
        void LogSubTotals();
        void IncrementSubTotal(ICalculationResult calculationResult);
    }
}
