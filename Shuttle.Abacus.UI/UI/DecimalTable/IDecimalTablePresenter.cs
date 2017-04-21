using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.UI
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
