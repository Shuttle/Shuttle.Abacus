namespace Shuttle.Abacus;

public class RecordNotFoundException(string message) : Exception(message)
{
    public static RecordNotFoundException For(string name, Guid id)
    {
        return new($"Could not find a record for '{name}' with id '{id}'.");
    }
}
