using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class Test
    {
        private readonly List<TestArgumentValue> _values = new List<TestArgumentValue>();

        public Test(Guid formulaId, string description, string expectedResult, string expectedResultType,
            string comparison)
            : this(Guid.NewGuid(), formulaId, description, expectedResult, expectedResultType, comparison)
        {
        }

        public Test(Guid id, Guid formulaId, string description, string expectedResult, string expectedResultType,
            string comparison)
        {
            Guard.AgainstNullOrEmptyString(description, "description");

            Id = id;
            FormulaId = formulaId;
            Description = description;
            ExpectedResult = expectedResult;
            ExpectedResultType = expectedResultType;
            Comparison = comparison;
        }

        public Guid Id { get; }
        public Guid FormulaId { get; }
        public string Description { get; }
        public string ExpectedResult { get; }
        public string ExpectedResultType { get; }
        public string Comparison { get; }

        public IEnumerable<TestArgumentValue> ArgumentValues => new ReadOnlyCollection<TestArgumentValue>(_values);

        public void AddArgumentAnswer(TestArgumentValue value)
        {
            _values.Add(value);
        }
    }
}