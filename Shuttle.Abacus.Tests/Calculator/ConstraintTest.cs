using System;
using NUnit.Framework;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class ConstraintTest
    {
        //TODO
        //[Test]
        //public void Should_be_able_to_use_equals()
        //{
        //    var formula = new Formula();

        //    formula.AddConstraint(new FromConstraint(Guid.NewGuid(), new IntegerValueType("Version", 6)));

        //    var context1 = new FormulaContext(string.Empty);

        //    context1.AddValue(new IntegerValueType("Version", 1));

        //    Assert.IsFalse(formula.IsSatisfiedBy(context1));

        //    var context2 = new FormulaContext(string.Empty);

        //    context2.AddValue(new IntegerValueType("Version", 6));

        //    Assert.IsTrue(formula.IsSatisfiedBy(context2));
        //}

        //[Test]
        //public void Should_be_able_to_use_from()
        //{
        //    var formula = new Formula();

        //    formula.AddConstraint(new FromConstraint(Guid.NewGuid(), new IntegerValueType("Version", 5)));

        //    var context1 = new FormulaContext(string.Empty);

        //    context1.AddValue(new IntegerValueType("Version", 1));

        //    Assert.IsFalse(formula.IsSatisfiedBy(context1));

        //    var context2 = new FormulaContext(string.Empty);

        //    context2.AddValue(new IntegerValueType("Version", 6));

        //    Assert.IsTrue(formula.IsSatisfiedBy(context2));
        //}

        //[Test]
        //public void Should_be_able_to_use_to()
        //{
        //    var formula = new Formula();

        //    formula.AddConstraint(new ToConstraint(Guid.NewGuid(), new IntegerValueType("Version", 5)));

        //    var context1 = new FormulaContext(string.Empty);

        //    context1.AddValue(new IntegerValueType("Version", 1));

        //    Assert.IsTrue(formula.IsSatisfiedBy(context1));

        //    var context2 = new FormulaContext(string.Empty);

        //    context2.AddValue(new IntegerValueType("Version", 6));

        //    Assert.IsFalse(formula.IsSatisfiedBy(context2));
        //}
    }
}
