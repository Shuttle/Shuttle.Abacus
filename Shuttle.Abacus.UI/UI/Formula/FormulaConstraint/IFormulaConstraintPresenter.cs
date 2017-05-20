using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.Formula.FormulaConstraint
{
    public interface IFormulaConstraintPresenter : IPresenter
    {
        void PopulateArgumentValues();
        bool ConstraintOK();
    }
}
