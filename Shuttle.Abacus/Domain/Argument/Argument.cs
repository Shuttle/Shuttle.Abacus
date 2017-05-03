using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shuttle.Abacus.Domain
{
    public class Argument
    {
        private static readonly List<string> Numbers = new List<string>
                                                       {
                                                           "decimal", "integer", "money"
                                                       };

        public static Argument MethodName = new Argument(Guid.Empty) { Name = "Method Name", AnswerType = "Text" };

        private readonly List<string> _answers = new List<string>();


        public Argument()
            : this(Guid.NewGuid())
        {
        }

        public Argument(Guid id)
        {
            Id = id;
        }

        public string Name { get; set; }
        public string AnswerType { get; set; }
        public bool IsSystemData { get; set; }

        public IEnumerable<string> RestrictedAnswers
        {
            get { return new ReadOnlyCollection<string>(_answers); }
        }

        public bool HasRestrictedAnswers
        {
            get { return _answers.Count > 0; }
        }

        public Guid Id { get; private set; }

        public Argument AddArgumentAnswer(string answer)
        {
            if (!ContainsAnswer(answer))
            {
                _answers.Add(answer);
            }

            return this;
        }

        public bool ContainsAnswer(string answer)
        {
            return _answers.Find(item=> item.Equals(answer)) != null;
        }

        public Argument AddArgumentAnswers(IEnumerable<string> answers)
        {
            foreach (var answer in answers)
            {
                AddArgumentAnswer(answer);
            }

            return this;
        }
    }
}
