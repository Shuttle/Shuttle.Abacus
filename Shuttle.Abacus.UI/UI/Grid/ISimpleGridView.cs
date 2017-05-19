using System.Data;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.Grid
{
    public interface ISimpleGridView : IView
    {
        void PopulateGridView(SimpleGridModel model);
        DataTable Items { get; }
    }
}
