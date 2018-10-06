using System;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentSearchSpecification
    {
        public Guid? Id { get; private set; }
        public string Name { get; private set; }

        public ArgumentSearchSpecification WithId(Guid id)
        {
            Id = id;

            return this;
        }

        public ArgumentSearchSpecification MatchingName(string name)
        {
            Name = name;

            return this;
        }
    }
}