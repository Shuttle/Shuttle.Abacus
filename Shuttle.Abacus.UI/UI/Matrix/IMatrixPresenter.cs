using System.Collections.Generic;
using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.Matrix
{
    public interface IMatrixPresenter : IPresenter
    {
        void MatrixNameExited();
        void RowArgumentChanged();
        void ColumnArgumentChanged();
        void ShowInvalidMatrixMessage();
        IEnumerable<string> ColumnValues();
        IEnumerable<string> RowValues();
        bool IsValidValue(string value);
        void ValueTypeChanged();
    }
}
