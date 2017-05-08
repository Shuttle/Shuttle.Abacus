using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.FormulaOperation
{
    public interface IFormulaOperationPresenter : IPresenter
    {
        void ValueSourceChanged();
        bool CanAddOperation();
    }
}
