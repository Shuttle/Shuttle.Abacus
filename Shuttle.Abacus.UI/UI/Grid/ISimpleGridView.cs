using System.Data;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Grid
{
    public interface ISimpleGridView : IView
    {
        void PopulateGridView(SimpleGridModel model);
        DataTable Items { get; }
    }
}
