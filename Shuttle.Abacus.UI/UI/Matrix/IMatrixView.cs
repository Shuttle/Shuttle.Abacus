using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Messages.v1.TransferObjects;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.DecimalTable
{
    public interface IMatrixView : IView
    {
        string DecimalTableNameValue { get; set; }
        IRuleCollection<object> DecimalTableNameRules { set; }
        IRuleCollection<object> RowArgumentRules { set; }
        ArgumentModel RowArgumentModel { get; }
        bool GridInitialized { get; }
        string RowArgumentValue { get; set; }
        string ColumnArgumentValue { get; set; }
        bool HasColumnArgument { get; }
        ArgumentModel ColumnArgumentModel { get; }
        void PopulateArguments(IEnumerable<ArgumentModel> rows);
        void EnableColumnArgument();
        void ShowRowAnswerCatalogConstraints();
        void ShowRowAllConstraints();
        void EnableRowAnswerSelection(IEnumerable<string> answers);
        void EnableRowAnswerEntry();
        void ShowColumnAnswerCatalogConstraints();
        void ShowColumnAllConstraints();
        void EnableColumnAnswerSelection(IEnumerable<string> answers);
        void EnableColumnAnswerEntry();
        void InitializeGrid();
        void RowFactorsOnly();
        void ApplyColumnArgument();
        bool HasInvalidDecimalTable();
        List<MatrixElement> DecimalValues();
        void AddElement(int column, int row, decimal value);
    }
}
