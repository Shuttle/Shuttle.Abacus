using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Formula
{
    public interface IFormulaView : IView
    {
        string ValueSourceValue { get; set; }
        ValueSourceTypeModel ValueSourceTypeModel { get; }
        string ValueSelectionValue { get; set; }
        string ValueSelectionText { get; }
        bool HasValueSource { get; }
        bool HasValueSelection { get; }
        bool HasOperation { get; }
        string OperationValue { get; set; }
        OperationTypeModel OperationTypeModel { get; }
        bool HasSelectedItem { get; }
        List<FormulaOperation> FormulaOperations { get; set; }
        string NameValue { get; set; }
        void PopulateValueSources(IEnumerable<ValueSourceTypeModel> models);
        void PopulateArguments(IEnumerable<ArgumentModel> arguments);
        void EnableValueSelection(string text);
        void EnableValueEntry(string text);
        void ShowValueSourceError();
        void ShowValueSelectionError(string message);

        void ShowOperationTypeError();
        void RemoveSelectedItem();
        void DisableValues();

        void AddOperation(string operationType, string valueSourceType, string valueSelection, string text);
        void PopulateOperations(IEnumerable<string> enumerable);
        void PopulateValues(IEnumerable<string> enumerable);
        void PopulateDecimalTables(IEnumerable<DecimalTableModel> list);
    }
}