/*
    This file forms part of Shuttle.Abacus.

    Shuttle.Abacus - A constraint-based calculation engine.
    Copyright (C) 2016  Eben Roux

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

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
