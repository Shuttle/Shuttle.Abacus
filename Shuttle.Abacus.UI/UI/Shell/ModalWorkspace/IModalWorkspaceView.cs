using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.Shell.ModalWorkspace
{
    public interface IModalWorkspaceView : IWorkspaceView
    {
        void Close();
        void SetText(string text);
    }
}
