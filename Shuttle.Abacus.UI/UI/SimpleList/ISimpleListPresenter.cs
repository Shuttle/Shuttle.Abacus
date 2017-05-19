using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.SimpleList
{
    public interface ISimpleListPresenter : 
        IPresenter
    {
        void DoubleClick();
        void Refresh();
    }
}
