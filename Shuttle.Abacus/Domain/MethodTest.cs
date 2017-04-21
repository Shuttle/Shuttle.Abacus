using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class MethodTest
    {
        private readonly List<MethodTestArgumentAnswer> answers = new List<MethodTestArgumentAnswer>();

        public MethodTest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public MethodTest(ICreateMethodTestCommand command)
        {
            MethodId = command.MethodId;
            Description = command.Description;
            ExpectedResult = command.ExpectedResult;

            foreach (var item in command.ArgumentAnswers)
            {
                AddArgumentAnswer(new MethodTestArgumentAnswer(item.ArgumentId, item.ArgumentName, item.AnswerType, item.Answer));
            }

            EnforceInvariants();
        }

        public MethodTest()
        {
        }

        public Guid MethodId { get; set; }
        public string Description { get; set; }
        public decimal ExpectedResult { get; set; }

        public IEnumerable<MethodTestArgumentAnswer> ArgumentAnswers
        {
            get { return new ReadOnlyCollection<MethodTestArgumentAnswer>(answers); }
        }

        public IEnumerable<MethodTestArgumentAnswer> Answers
        {
            get { return new ReadOnlyCollection<MethodTestArgumentAnswer>(answers); }
        }

        private void EnforceInvariants()
        {
            Guard.Against<InvalidStateException>(Id.Equals(Guid.Empty), "An ID is required.");
            Guard.Against<InvalidStateException>(string.IsNullOrEmpty(Description), "The description may not be empty.");
        }

        public MethodTest ProcessCommand(IChangeMethodTestCommand command)
        {
            Description = command.Description;
            ExpectedResult = command.ExpectedResult;

            answers.Clear();

            foreach (var item in command.ArgumentAnswers)
            {
                AddArgumentAnswer(new MethodTestArgumentAnswer(item.ArgumentId, item.ArgumentName, item.AnswerType, item.Answer));
            }

            EnforceInvariants();

            return this;
        }

        public void AddArgumentAnswer(MethodTestArgumentAnswer answer)
        {
            answers.Add(answer);
        }
    }
}
