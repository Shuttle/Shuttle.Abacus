using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Constraint
{
    public interface IConstraintView : IView
    {
        void PopulateArguments(IEnumerable<ArgumentModel> items);
        void SetContraintTypes(IEnumerable<ConstraintTypeModel> list);
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
        List<ConstraintModel> Constraints { get; set; }
        
        ComboBox ValueSelectionControl { get; }
        TextBox FormattedControl { get; }

        void ShowAnswerError(string message);
        void ShowArgumentError();
        void ShowConstraintError();
        void AddConstraint(Guid argumentId, string argumentName, string constraintName, string valueSelection);
        void ShowAllConstraints();
        void ShowAnswerCatalogConstraints();
        
        void AttachValueFormatter(MoneyFormatter formatter);
        void DetachValueFormatter();
    }
}
