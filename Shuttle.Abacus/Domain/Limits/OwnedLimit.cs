using System;

namespace Shuttle.Abacus.Domain
{
    public class OwnedLimit
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }

        public OwnedLimit(Guid id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}