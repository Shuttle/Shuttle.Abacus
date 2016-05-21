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
