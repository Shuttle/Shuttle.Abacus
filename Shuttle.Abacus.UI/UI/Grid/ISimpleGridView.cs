using System.Data;

namespace Abacus.UI
{
    public interface ISimpleGridView : IView
    {
        void PopulateGridView(SimpleGridModel model);
        DataTable Items { get; }
    }
}
