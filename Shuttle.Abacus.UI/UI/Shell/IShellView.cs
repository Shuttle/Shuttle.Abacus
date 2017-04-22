using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Navigation;
using Shuttle.Abacus.UI.UI.Shell.Explorer;

namespace Shuttle.Abacus.UI.UI.Shell
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
