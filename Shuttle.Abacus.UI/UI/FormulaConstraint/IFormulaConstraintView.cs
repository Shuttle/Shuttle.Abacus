using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.FormulaConstraint
{
    public interface IFormulaConstraintView : IView
    {
        ArgumentModel ArgumentModel { get; }
        string ComparisonValue { get; }
        string ArgumentValue { get; set; }
        bool HasArgumentValue { get; }
        bool HasArgument { get; }

        bool HasComparison { get; }

        IEnumerable<FormulaConstraintModel> FormulaConstraints { get; set; }
        void PopulateArguments(IEnumerable<ArgumentModel> items);
        void PopulateArgumentValues(IEnumerable<string> values);
        void ShowArgumentValueError(string message);
        void ShowArgumentError();

        void ShowComparisonError();
        void AddConstraint(string argumentName, string comparison, string value);
    }
}