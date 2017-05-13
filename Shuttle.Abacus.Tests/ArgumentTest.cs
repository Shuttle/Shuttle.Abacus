using System.Collections.Generic;
using NUnit.Framework;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class ArgumentTest
    {
        //[Test]
        //public void Should_be_able_to_add_correct_number_of_entries()
        //{
        //    var command = new CreateArgumentCommand
        //                  {
        //                      Name = "One", 
        //                      ValueType = "Boolean",
        //                      ArgumentValues = new List<ArgumentRestrictedAnswerDTO>()
        //                  };

        //    command.ArgumentValues.Add(new ArgumentRestrictedAnswerDTO("One"));
        //    command.ArgumentValues.Add(new ArgumentRestrictedAnswerDTO("Two"));
        //    command.ArgumentValues.Add(new ArgumentRestrictedAnswerDTO("Three"));

        //    Assert.AreEqual(3, command.ArgumentValues.Count);
        //}

        //[Test]
        //public void Should_not_be_able_to_add_duplicate_answers()
        //{
        //    var command = new CreateArgumentCommand
        //                  {
        //                      Name = "One", 
        //                      ValueType = "Boolean",
        //                      ArgumentValues = new List<ArgumentRestrictedAnswerDTO>()
        //                  };

        //    command.ArgumentValues.Add(new ArgumentRestrictedAnswerDTO("One"));
        //    command.ArgumentValues.Add(new ArgumentRestrictedAnswerDTO("One"));
        //    command.ArgumentValues.Add(new ArgumentRestrictedAnswerDTO("Three"));

        //    Assert.Throws<DuplicateEntryException>(() => new Argument().ProcessCommand(command));
        //}
    }
}
