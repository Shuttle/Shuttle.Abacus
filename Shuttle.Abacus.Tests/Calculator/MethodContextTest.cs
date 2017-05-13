using NUnit.Framework;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class MethodContextTest
    {
        //TODO
        //[Test]
        //public void Should_be_able_to_add_and_find_argument_answers()
        //{
        //    var context = new FormulaContext(string.Empty);

        //    context.AddValue(new DecimalValueType("SumInsured", 300000));

        //    Assert.AreSame(ValueType.Null, context.GetArgumentAnswer("blah"));
        //    Assert.AreEqual(300000, context.GetArgumentAnswer("SumInsured").Answer);
        //}

        //[Test]
        //public void Should_be_able_to_add_and_retrieve_a_named_calculation_result()
        //{
        //    var methodContext = new FormulaContext(string.Empty);

        //    var result1 = new CalculationCollectionResult(new CalculationCollection("result1"), 123.45m);

        //    methodContext.AddResult(result1);
        //    methodContext.IncrementSubTotal(result1);

        //    var result2 = new CalculationCollectionResult(new CalculationCollection("result2"), 123.45m);

        //    methodContext.AddResult(result2);
        //    methodContext.IncrementSubTotal(result2);

        //    Assert.AreEqual(123.45m, methodContext.GetResult("result1").Value);
        //    Assert.AreEqual(123.45m, methodContext.GetResult("result2").Value);

        //    Assert.AreEqual(246.9m, methodContext.Total.Value);
        //}

        //[Test]
        //public void Should_be_able_to_create_a_read_only_copy()
        //{
        //    var methodContext = new FormulaContext(string.Empty);

        //    methodContext.AddValue(new IntegerValueType("input", 10));

        //    methodContext.IncrementSubTotal(new CalculationCollectionResult(new CalculationCollection("result"), 25));
        //    methodContext.AddResult(new FormulaCalculationResult(new FormulaCalculation("result"), 25));

        //    var copy = methodContext.Copy().AsReadOnly();

        //    Assert.IsTrue(copy.HasArgumentAnswer("input"));
        //    Assert.AreEqual(10, (int)copy.GetArgumentAnswer("input").Answer);

        //    Assert.IsTrue(copy.HasResult("result"));
        //    Assert.AreEqual(25, copy.GetResult("result").Value);
        //    Assert.AreEqual(25, copy.GetSubTotal("result").Value);

        //    Assert.AreEqual(25, copy.Total.Value);

        //    copy.IncrementSubTotal(new CalculationCollectionResult(new CalculationCollection("result2"), 25));
        //    copy.AddResult(new FormulaCalculationResult(new FormulaCalculation("result2"), 25));

        //    Assert.IsFalse(copy.HasResult("result2"));
        //    Assert.AreEqual(25, copy.Total.Value);
        //}

    }
}
