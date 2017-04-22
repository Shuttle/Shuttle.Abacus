using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.List
{
    public interface ISimpleListPresenter : 
        IPresenter
    {
        void DoubleClick();
        void Refresh();
    }
}
