using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Invariants.Interfaces
{
    public interface IValueTypeRules
    {
        IRuleCollection<object> BooleanRules();
        IRuleCollection<object> DateTimeRules();
        IRuleCollection<object> DecimalRules();
        IRuleCollection<object> IntegerRules();
        IRuleCollection<object> TextRules();
        IRuleCollection<object> For(string type);
    }
}
