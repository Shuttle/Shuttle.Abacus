using System.Collections.Generic;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.Matrix
{
    public interface IMatrixPresenter : IPresenter
    {
        void MatrixNameExited();
        void RowArgumentChanged();
        void ColumnArgumentChanged();
        bool IsValidValue(object value);
        void ShowInvalidMatrixMessage();
        IEnumerable<string> ColumnAnswers();
        IEnumerable<string> RowAnswers();
    }
}
