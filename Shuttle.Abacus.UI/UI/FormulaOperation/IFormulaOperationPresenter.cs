using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.FormulaOperation
{
    public interface IFormulaOperationPresenter : IPresenter
    {
        void ValueSourceChanged();
        bool CanAddOperation();
    }
}
