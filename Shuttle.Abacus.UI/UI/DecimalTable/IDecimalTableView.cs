using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.DecimalTable
{
    public interface IDecimalTableView : IView
    {
        string DecimalTableNameValue { get; set; }
        IRuleCollection<object> DecimalTableNameRules { set; }
        IRuleCollection<object> RowArgumentRules { set; }
        DataRow RowArgumentDto { get; }
        bool GridInitialized { get; }
        string RowArgumentValue { get; set; }
        string ColumnArgumentValue { get; set; }
        bool HasColumnArgument { get; }
        DataRow ColumnDataRow { get; }
        void PopulateArguments(IEnumerable<DataRow> rows);
        void EnableColumnArgument();
        void ShowRowAnswerCatalogConstraints();
        void ShowRowAllConstraints();
        void EnableRowAnswerSelection(List<ArgumentRestrictedAnswerDTO> answers);
        void EnableRowAnswerEntry();
        void ShowColumnAnswerCatalogConstraints();
        void ShowColumnAllConstraints();
        void EnableColumnAnswerSelection(List<ArgumentRestrictedAnswerDTO> answers);
        void EnableColumnAnswerEntry();
        void InitializeGrid();
        void RowFactorsOnly();
        void ApplyColumnArgument();
        bool HasInvalidDecimalTable();
        List<DecimalValueDTO> DecimalValueDTOs();
        void AddDecimalValue(int column, int row, decimal value, string constraint, string argument, string answer);
    }
}
