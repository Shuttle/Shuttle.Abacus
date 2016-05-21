using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shuttle.Abacus
{
    public class Argument
    {
        public static Argument MethodName = new Argument(Guid.Empty) { Name = "Method Name", AnswerType = "Text" };

        private readonly List<ArgumentRestrictedAnswer> answers = new List<ArgumentRestrictedAnswer>();

        public Argument(Guid id)
        {
            Id = id;
        }

        public Argument(ICreateArgumentCommand command)
        {
            Name = command.Name;
            AnswerType = command.AnswerType;

            command.Answers.ForEach(adding => AddArgumentAnswer(adding.Answer));
        }

        public Argument()
        {
        }

        public string Name { get; set; }
        public string AnswerType { get; set; }
        public bool IsSystemData { get; set; }

        public IEnumerable<ArgumentRestrictedAnswer> RestrictedAnswers
        {
            get { return new ReadOnlyCollection<ArgumentRestrictedAnswer>(answers); }
        }

        public bool HasRestrictedAnswers
        {
            get { return answers.Count > 0; }
        }

        public Guid Id { get; private set; }

        public Argument ProcessCommand(IChangeArgumentCommand command)
        {
            Name = command.Name;
            AnswerType = command.AnswerType;

            answers.Clear();

            command.ArgumentAnswers.ForEach(adding => AddArgumentAnswer(adding.Answer));

            return this;
        }

        public void AddArgumentAnswer(string answer)
        {
            answers.ForEach
                (
                current =>
                {
                    if (current.Answer.Equals(answer, StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new DuplicateEntryException(string.Format("Duplicate answer '{0}'.", answer));
                    }
                }
                );

            answers.Add(new ArgumentRestrictedAnswer(answer));
        }

        public ArgumentRestrictedAnswer FindAnswer(string answer)
        {
            return answers.Find(item => item.Answer.Equals(answer, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
