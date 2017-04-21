namespace Abacus.UI
{
    public interface IConstraintPresenter : IPresenter
    {
        void ArgumentChanged();
        bool ConstraintOK();
    }
}
