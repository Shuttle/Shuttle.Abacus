namespace Abacus.UI
{
    public interface INavigationItemConfiguration<T> 
        where T : class 
    {
        T AsDefault();
        T AsCancel();
    }
}
