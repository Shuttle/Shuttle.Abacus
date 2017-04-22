using System.Collections.Generic;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.DecimalTable
{
    public interface IDecimalTablePresenter : IPresenter
    {
        IEnumerable<ConstraintTypeDTO> ConstraintTypes { get; }
        void DecimalTableNameExited();
        void RowArgumentChanged();

        void ColumnArgumentChanged();
        bool IsDecimal(string value);
        bool IsValidAnswer(ArgumentDTO argumentDto, object value);
        void ShowInvalidDecimalTableMessage();
        ConstraintTypeDTO GetConstraintType(string name);
    }
}
