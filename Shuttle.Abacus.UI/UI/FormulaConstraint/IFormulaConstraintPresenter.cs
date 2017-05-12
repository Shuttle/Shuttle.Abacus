using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.FormulaConstraint
{
    public interface IFormulaConstraintPresenter : IPresenter
    {
        void ArgumentChanged();
        bool ConstraintOK();
    }
}
