namespace Shuttle.Abacus.UI.Core.Presentation
{
    public interface IWorkspaceProvider
    {
        T Get<T>() where T : IWorkspacePresenter;
        T RegisterSingleton<T>() where T : IWorkspacePresenter;
    }
}
