using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Constraint
{
    public interface IConstraintView : IView
    {
        void PopulateFactors(IEnumerable<ArgumentDTO> items);
        void SetContraintTypes(IEnumerable<ConstraintTypeDTO> items);
        ArgumentDTO ArgumentDto { get; }
        ConstraintTypeDTO ConstraintTypeDTO { get; }
        void EnableAnswerSelection();
        void EnableAnswerEntry();
        void PopulateAnswerCatalogValues(IEnumerable<ArgumentRestrictedAnswerDTO> list);
        string AnswerValue { get; set; }
        bool HasAnswer { get; }
        bool HasArgument { get; }
        bool HasConstraint { get; }
        List<ConstraintDTO> Constraints { get; set; }
        
        ComboBox ValueSelectionControl { get; }
        TextBox FormattedControl { get; }

        void ShowAnswerError(string message);
        void ShowArgumentError();
        void ShowConstraintError();
        void AddConstraint(ArgumentDTO argumentDto, ConstraintTypeDTO constraintTypeDTO, string valueSelection);
        void ShowAllConstraints();
        void ShowAnswerCatalogConstraints();
        
        void AttachValueFormatter(MoneyFormatter formatter);
        void DetachValueFormatter();
    }
}
