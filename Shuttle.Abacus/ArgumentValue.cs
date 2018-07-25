using System;

namespace Shuttle.Abacus
{
    public class ArgumentValue
    {
        public ArgumentValue(Guid id, string value)
        {
            Id = id;
            Value = value;
        }

        public Guid Id { get; }
        public string Value { get; }
    }
}