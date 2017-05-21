namespace Shuttle.Abacus
{
    public interface IContext
    {
        string FormulaName { get; }

        ExecutionContext SetValue(string name, string value);
        string GetValue(string name);
    }
}