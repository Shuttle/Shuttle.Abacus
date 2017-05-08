using System;

namespace Shuttle.Abacus.Domain
{
    public class TestItem
    {
        public TestItem(Guid id, decimal expectedResult, string description)
        {
            Id = id;
            ExpectedResult = expectedResult;
            Description = description;
        }

        public Guid Id { get; }
        public decimal ExpectedResult { get; }
        public string Description { get; }
    }
}