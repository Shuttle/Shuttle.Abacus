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

using System.Collections.Generic;
using NUnit.Framework;
using Shuttle.Abacus;

namespace Abacus.Test.Unit
{
    [TestFixture]
    public class ArgumentTest
    {
        [Test]
        public void Should_be_able_to_add_correct_number_of_entries()
        {
            var command = new CreateArgumentCommand
                          {
                              Name = "One", 
                              AnswerType = "Boolean",
                              Answers = new List<ArgumentRestrictedAnswerDTO>()
                          };

            command.Answers.Add(new ArgumentRestrictedAnswerDTO("One"));
            command.Answers.Add(new ArgumentRestrictedAnswerDTO("Two"));
            command.Answers.Add(new ArgumentRestrictedAnswerDTO("Three"));

            Assert.AreEqual(3, command.Answers.Count);
        }

        [Test]
        public void Should_not_be_able_to_add_duplicate_answers()
        {
            var command = new CreateArgumentCommand
                          {
                              Name = "One", 
                              AnswerType = "Boolean",
                              Answers = new List<ArgumentRestrictedAnswerDTO>()
                          };

            command.Answers.Add(new ArgumentRestrictedAnswerDTO("One"));
            command.Answers.Add(new ArgumentRestrictedAnswerDTO("One"));
            command.Answers.Add(new ArgumentRestrictedAnswerDTO("Three"));

            Assert.Throws<DuplicateEntryException>(() => new Argument(command));
        }
    }
}
