namespace Abacus.UI
{
    public interface IFormulaPresenter : IPresenter
    {
        void ValueSourceChanged();
        bool CanAddOperation();
    }
}
