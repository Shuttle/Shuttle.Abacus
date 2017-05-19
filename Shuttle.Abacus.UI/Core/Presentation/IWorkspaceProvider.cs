namespace Shuttle.Abacus.Shell.Core.Presentation
{
    public interface IWorkspaceProvider
    {
        T Get<T>() where T : IWorkspacePresenter;
        T RegisterSingleton<T>() where T : IWorkspacePresenter;
    }
}
