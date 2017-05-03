using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class MethodTest
    {
        private readonly List<MethodTestArgumentAnswer> answers = new List<MethodTestArgumentAnswer>();

        public MethodTest(Guid methodId, string description, decimal expectedResult)
            : this(Guid.NewGuid(), methodId, description, expectedResult)
        {
        }

        public MethodTest(Guid id, Guid methodId, string description, decimal expectedResult)
        {
            Guard.AgainstNullOrEmptyString(description, "description");

            Id = id;
            MethodId = methodId;
            Description = description;
            ExpectedResult = expectedResult;
        }

        public Guid Id { get; private set; }
        public Guid MethodId { get; private set; }
        public string Description { get; private set; }
        public decimal ExpectedResult { get; private set; }

        public IEnumerable<MethodTestArgumentAnswer> ArgumentAnswers
        {
            get { return new ReadOnlyCollection<MethodTestArgumentAnswer>(answers); }
        }

        public IEnumerable<MethodTestArgumentAnswer> Answers
        {
            get { return new ReadOnlyCollection<MethodTestArgumentAnswer>(answers); }
        }

        public void AddArgumentAnswer(MethodTestArgumentAnswer answer)
        {
            answers.Add(answer);
        }
    }
}
