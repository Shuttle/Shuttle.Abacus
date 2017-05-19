namespace Shuttle.Abacus.Shell.Core.WorkItem
{
    public interface INavigationItemConfiguration<T> 
        where T : class 
    {
        T AsDefault();
        T AsCancel();
    }
}
