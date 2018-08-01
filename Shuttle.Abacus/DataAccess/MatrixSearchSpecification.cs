using System;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixSearchSpecification
    {
        public Guid? Id { get; private set; }
        public string Name { get; private set; }

        public MatrixSearchSpecification WithId(Guid id)
        {
            Id = id;

            return this;
        }

        public MatrixSearchSpecification MatchingName(string name)
        {
            Name = name;

            return this;
        }
    }
}