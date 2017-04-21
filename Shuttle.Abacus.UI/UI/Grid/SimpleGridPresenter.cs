using Abacus.Infrastructure;

namespace Abacus.UI
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
