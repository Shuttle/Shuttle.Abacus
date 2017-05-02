using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Formula
{
    public interface IFormulaView : IView
    {
        string ValueSourceValue { get; set; }
        ValueSourceTypeDTO ValueSourceTypeDTO { get; }
        string ValueSelectionValue { get; set; }
        string ValueSelectionText { get; }
        bool HasValueSource { get; }
        bool HasValueSelection { get; }
        bool HasOperation { get; }
        string OperationValue { get; set; }
        OperationTypeDTO OperationTypeDTO { get; }
        bool HasSelectedItem { get; }
        List<FormulaOperation> FormulaOperations { get; set; }
        void PopulateValueSources(IEnumerable<ValueSourceTypeDTO> enumerable);
        void PopulateArguments(IEnumerable<DataRow> rows);
        void EnableValueSelection(string text);
        void EnableValueEntry(string text);
        void ShowValueSourceError();
        void ShowValueSelectionError(string message);
        void PopulatePrecedingCalculations(IEnumerable<CalculationDTO> enumerable);

        void ShowOperationTypeError();
        void RemoveSelectedItem();
        void DisableValues();

        void AddOperation(OperationTypeDTO operationType, ValueSourceTypeDTO valueSourceType, string valueSelection, string text);
        void PopulateOperations(IEnumerable<OperationTypeDTO> enumerable);
        void PopulateValues(IEnumerable<string> enumerable);
        void PopulateDecimalTables(IEnumerable<DecimalTableModel> list);
        void PopulateMethods(IEnumerable<MethodDTO> enumerable);
    }
}
