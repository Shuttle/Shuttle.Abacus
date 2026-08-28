// Dead code kept for reference: an earlier, unfinished value-provider abstraction. `ExecutionService` resolves
// operation values with a plain string switch on `ValueProviderType` instead of this interface.
namespace Shuttle.Abacus;

public interface IValueProvider
{
    string Name { get; }
    string Value(string inputParameter);
}
