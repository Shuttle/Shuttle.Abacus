using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.Section;

namespace Shuttle.Abacus.UI.UI.Method
{
    public interface IMethodPresenter :
        IPresenter
    {
        void TitleExited();

        void HandleMessage(EditMethodMessage message);
    }
}
