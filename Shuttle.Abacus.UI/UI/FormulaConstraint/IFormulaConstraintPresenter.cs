using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.FormulaConstraint
{
    public interface IFormulaConstraintPresenter : IPresenter
    {
        void PopulateArgumentValues();
        bool ConstraintOK();
    }
}
