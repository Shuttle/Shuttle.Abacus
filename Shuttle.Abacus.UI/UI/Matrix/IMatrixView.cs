using System.Collections;
using System.Collections.Generic;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Messages.v1.TransferObjects;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.Matrix
{
    public interface IMatrixView : IView
    {
        string MatrixNameValue { get; set; }
        IRuleCollection<object> NameRules { set; }
        ArgumentModel RowArgumentModel { get; }
        bool GridInitialized { get; }
        string RowArgumentValue { get; set; }
        string ColumnArgumentValue { get; set; }
        bool HasColumnArgument { get; }
        ArgumentModel ColumnArgumentModel { get; }
        string ValueTypeValue { get; set; }
        bool HasValueType { get; }
        void PopulateArguments(IEnumerable<ArgumentModel> models);
        void EnableColumnArgument();
        void InitializeGrid();
        void RowValuesOnly();
        void ApplyColumnArgument();
        bool HasValidMatrix();
        List<MatrixElement> Elements();
        void AddElement(int column, int row, decimal value);
        void ValidateMatrix();
        List<MatrixConstraint> Constraints();
    }
}
