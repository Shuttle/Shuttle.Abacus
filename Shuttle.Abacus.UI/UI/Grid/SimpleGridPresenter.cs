using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Grid
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

            Guard.AgainstNullDependency(Model, "Model");

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
