using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Shell.ModalWorkspace
{
    public interface IModalWorkspaceView : IWorkspaceView
    {
        void Close();
        void SetText(string text);
    }
}
