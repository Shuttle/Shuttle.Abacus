using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Matrix
{
    public interface IMatrixPresenter : IPresenter
    {
        IEnumerable<ConstraintTypeModel> ConstraintTypes { get; }
        void DecimalTableNameExited();
        void RowArgumentChanged();
        void ColumnArgumentChanged();
        bool IsDecimal(string value);
        bool IsValidAnswer(ArgumentModel model, object value);
        void ShowInvalidDecimalTableMessage();
        IEnumerable<string> ColumnAnswers();
        IEnumerable<string> RowAnswers();
    }
}
