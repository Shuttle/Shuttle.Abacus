namespace Abacus.UI
{
    public interface IModalWorkspaceView : IWorkspaceView
    {
        void Close();
        void SetText(string text);
    }
}
