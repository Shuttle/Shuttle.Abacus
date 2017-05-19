using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.UI.Grid
{
    public class SimpleGridPresenter : Presenter<ISimpleGridView, SimpleGridModel>, ISimpleGridPresenter
    {
        public SimpleGridPresenter(ISimpleGridView view)
            : base(view)
        {
            Text = "Grid";
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            if (Model == null)
            {
                throw new NullDependencyException("Model");
            }

            Refresh();
        }

        public void Refresh()
        {
            if (Model == null)
            {
                return;
            }

            View.PopulateGridView(Model);
        }
    }
}
