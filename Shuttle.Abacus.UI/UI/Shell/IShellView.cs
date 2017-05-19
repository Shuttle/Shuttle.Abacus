using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Navigation;
using Shuttle.Abacus.Shell.UI.Shell.Explorer;

namespace Shuttle.Abacus.Shell.UI.Shell
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
