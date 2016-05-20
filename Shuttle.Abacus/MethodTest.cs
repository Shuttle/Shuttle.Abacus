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
