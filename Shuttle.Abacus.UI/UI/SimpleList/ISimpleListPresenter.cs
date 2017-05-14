using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.SimpleList
{
    public interface ISimpleListPresenter : 
        IPresenter
    {
        void DoubleClick();
        void Refresh();
    }
}
