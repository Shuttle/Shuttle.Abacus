namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public interface INavigationItemConfiguration<T> 
        where T : class 
    {
        T AsDefault();
        T AsCancel();
    }
}
