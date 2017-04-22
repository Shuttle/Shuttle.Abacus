using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Constraint
{
    public interface IConstraintPresenter : IPresenter
    {
        void ArgumentChanged();
        bool ConstraintOK();
    }
}
