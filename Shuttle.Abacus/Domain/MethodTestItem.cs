using System;

namespace Shuttle.Abacus.Domain
{
    public class MethodTestItem
    {
        public MethodTestItem(Guid id, decimal expectedResult, string description)
        {
            Id = id;
            ExpectedResult = expectedResult;
            Description = description;
        }

        public Guid Id { get; private set; }
        public decimal ExpectedResult { get; private set; }
        public string Description { get; private set; }
    }
}