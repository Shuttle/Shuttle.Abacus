using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.FormulaConstraint
{
    public interface IFormulaConstraintView : IView
    {
        void PopulateArguments(IEnumerable<ArgumentModel> items);
        ArgumentModel ArgumentModel { get; }
        string ComparisonValue { get; }
        void PopulateArgumentValues(IEnumerable<DataRow> rows);
        string AnswerValue { get; set; }
        bool HasAnswer { get; }
        bool HasArgument { get; }
        bool HasConstraint { get; }
        IEnumerable<FormulaConstraintModel> FormulaConstraints { get; set; }

        ComboBox ValueSelectionControl { get; }

        void ShowAnswerError(string message);
        void ShowArgumentError();
        void ShowConstraintError();
        void AddConstraint(string argumentName, string comparison, string value);
        void ShowAllConstraints();
    }
}
