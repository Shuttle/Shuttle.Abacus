using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Formula
{
    public interface IFormulaPresenter : IPresenter
    {
        void ValueSourceChanged();
        bool CanAddOperation();
    }
}
