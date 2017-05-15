using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.FormulaConstraint
{
    public interface IFormulaConstraintView : IView
    {
        void PopulateArguments(IEnumerable<ArgumentModel> items);
        void PopulateContraintTypes(IEnumerable<string> constraintTypes);
        ArgumentModel ArgumentModel { get; }
        ConstraintTypeModel ConstraintTypeModel { get; }
        void EnableAnswerSelection();
        void EnableAnswerEntry();
        void PopulateAnswers(IEnumerable<DataRow> rows);
        string AnswerValue { get; set; }
        bool HasAnswer { get; }
        bool HasAnswers { get; }
        bool HasArgument { get; }
        bool HasConstraint { get; }
        List<FormulaConstraintModel> Constraints { get; set; }
        
        ComboBox ValueSelectionControl { get; }
        TextBox FormattedControl { get; }

        void ShowAnswerError(string message);
        void ShowArgumentError();
        void ShowConstraintError();
        void AddConstraint(string argumentName, string comparisonType, string value);
        void ShowAllConstraints();
        void ShowAnswerCatalogConstraints();
        
        void AttachValueFormatter(MoneyFormatter formatter);
        void DetachValueFormatter();
    }
}
