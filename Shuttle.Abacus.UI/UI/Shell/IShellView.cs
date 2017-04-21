using System.Collections.Generic;
using System.Windows.Forms;

namespace Abacus.UI
{
    public interface IShellView : IView
    {
        void PopulateMenu(IEnumerable<INavigationItem> items);
        void ShowContainer(UserControl container);
        void Busy();
        void Ready();
        void ShowStatus(string message);
        IExplorerPartialView ExplorerView { get; }
    }
}
