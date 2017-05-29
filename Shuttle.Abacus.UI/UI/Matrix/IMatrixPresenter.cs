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
        bool IsDecimal(string value);
        bool IsValidAnswer(ArgumentModel model, object value);
        void ShowInvalidDecimalTableMessage();
        IEnumerable<string> ColumnAnswers();
        IEnumerable<string> RowAnswers();
    }
}
