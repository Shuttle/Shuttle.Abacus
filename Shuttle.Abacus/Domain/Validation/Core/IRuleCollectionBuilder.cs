namespace Shuttle.Abacus
{
    public interface IRuleCollectionBuilder
    {
        IRuleCollectionBuilder MinimumLength(int minimumLength);
        IRuleCollectionBuilder MaximumLength(int maximumLength);
        IRuleCollectionBuilder Required();
        IRuleCollectionBuilder Decimal();
        IRuleCollectionBuilder DateTime();
        IRuleCollectionBuilder Integer();

        IRuleCollection<object> Create();
    }
}
