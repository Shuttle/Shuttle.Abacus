using System;

namespace Shuttle.Abacus
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(string message) : base(message)
        {
        }

        public static RecordNotFoundException For(string name, Guid id)
        {
            return new RecordNotFoundException($"Could not find a record for '{name}' with id '{id}'.");
        }
    }
}