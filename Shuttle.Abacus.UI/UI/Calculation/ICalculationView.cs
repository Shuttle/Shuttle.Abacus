using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Calculation
{
    public interface ICalculationView : IView
    {
        string CalculationNameValue { get; set; }
        string TypeValue { get; set; }
        bool RequiredValue { get; set; }

        IRuleCollection<object> CalculationNameRules { set; }
        IRuleCollection<object> TypeRules { set; }

        void DisableForEditing();
        void EnableFormulaInputs();
        void DisableFormulaInputs();
    }
}
