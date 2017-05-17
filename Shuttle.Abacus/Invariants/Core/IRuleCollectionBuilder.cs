namespace Shuttle.Abacus.Invariants.Core
{
    public interface IRuleCollectionBuilder
    {
        IRuleCollectionBuilder MinimumLength(int minimumLength);
        IRuleCollectionBuilder MaximumLength(int maximumLength);
        IRuleCollectionBuilder Required();
        IRuleCollectionBuilder Boolean();
        IRuleCollectionBuilder Decimal();
        IRuleCollectionBuilder DateTime();
        IRuleCollectionBuilder Integer();

        IRuleCollection<object> Create();
    }
}
