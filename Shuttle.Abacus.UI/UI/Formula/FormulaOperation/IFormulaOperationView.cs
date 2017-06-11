using System.Collections.Generic;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.Formula.FormulaOperation
{
    public interface IFormulaOperationView : IView
    {
        string ValueSourceValue { get; set; }
        string ValueSelectionValue { get; set; }
        string ValueSelectionText { get; }
        bool HasValueSource { get; }
        bool HasValueSelection { get; }
        bool HasOperation { get; }
        string OperationValue { get; set; }
        OperationTypeModel OperationTypeModel { get; }
        bool HasSelectedItem { get; }
        IEnumerable<FormulaOperationModel> FormulaOperations { get; set; }
        string NameValue { get; set; }
        void PopulateArguments(IEnumerable<ArgumentModel> arguments);
        void ShowValueSourceError();
        void ShowValueSelectionError(string message);

        void ShowOperationTypeError();
        void RemoveSelectedItem();
        void DisableValues();

        void AddOperation(string operation, string valueSource, string valueSelection);
        void PopulateValues(IEnumerable<string> values);
        void PopulateMatrixes(IEnumerable<MatrixModel> matrixes);
        void ClearValueSelection();
        void PopulateFormulas(IEnumerable<FormulaModel> formulas);
    }
}