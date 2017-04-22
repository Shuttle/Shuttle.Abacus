using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Presentation;

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
        List<OperationDTO> Operations { get; set; }
        void PopulateValueSources(IEnumerable<ValueSourceTypeDTO> enumerable);
        void PopulateFactors(IEnumerable<ArgumentDTO> enumerable);
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
        void PopulateDecimalTables(IEnumerable<DecimalTableDTO> enumerable);
        void PopulateMethods(IEnumerable<MethodDTO> enumerable);
    }
}
