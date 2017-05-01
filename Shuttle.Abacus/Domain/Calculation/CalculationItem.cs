using System;

namespace Shuttle.Abacus.Domain
{
    public class CalculationItem
    {
        public CalculationItem(Guid id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
    }
}