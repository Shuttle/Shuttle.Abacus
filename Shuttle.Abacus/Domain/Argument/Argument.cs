using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class Argument
    {
        private static readonly List<string> Numbers = new List<string>
                                                       {
                                                           "decimal", "integer", "money"
                                                       };

        public static Argument MethodName = new Argument(Guid.Empty) { Name = "Method Name", AnswerType = "Text" };

        private readonly List<ArgumentRestrictedAnswer> _answers = new List<ArgumentRestrictedAnswer>();


        public Argument()
            : this(Guid.NewGuid())
        {
        }

        public Argument(Guid id)
        {
            Id = id;
        }

        public Argument ProcessCommand(CreateArgumentCommand command)
        {
            Name = command.Name;
            AnswerType = command.AnswerType;

            command.Answers.ForEach(adding => AddArgumentAnswer(adding.Answer));

            return this;
        }

        public string Name { get; set; }
        public string AnswerType { get; set; }
        public bool IsSystemData { get; set; }

        public IEnumerable<ArgumentRestrictedAnswer> RestrictedAnswers
        {
            get { return new ReadOnlyCollection<ArgumentRestrictedAnswer>(_answers); }
        }

        public bool HasRestrictedAnswers
        {
            get { return _answers.Count > 0; }
        }

        public Guid Id { get; private set; }

        public Argument ProcessCommand(ChangeArgumentCommand command)
        {
            Name = command.Name;
            AnswerType = command.AnswerType;

            _answers.Clear();

            command.ArgumentAnswers.ForEach(adding => AddArgumentAnswer(adding.Answer));

            return this;
        }

        public void AddArgumentAnswer(string answer)
        {
            _answers.ForEach
                (
                current =>
                {
                    if (current.Answer.Equals(answer, StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new DuplicateEntryException(string.Format("Duplicate answer '{0}'.", answer));
                    }
                }
                );

            _answers.Add(new ArgumentRestrictedAnswer(answer));
        }

        public ArgumentRestrictedAnswer FindAnswer(string answer)
        {
            return _answers.Find(item => item.Answer.Equals(answer, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool IsNumber
        {
            get { return !string.IsNullOrEmpty(AnswerType) && Numbers.Contains(AnswerType.ToLower()); }
        }

        public bool HasAnswerCatalog
        {
            get { return _answers != null && _answers.Count > 0; }
        }

        public bool IsMoney
        {
            get { return AnswerType.ToLower() == "money"; }
        }

        public bool IsText
        {
            get { return AnswerType.ToLower() == "text"; }
        }

        public bool CanOnlyCompareEquality
        {
            get { return HasAnswerCatalog || IsText; }
        }

        public bool HasArgumentName(string name)
        {
            return _answers.Find(item => item.Answer.Equals(name, StringComparison.InvariantCultureIgnoreCase)) != null;
        }
    }
}
