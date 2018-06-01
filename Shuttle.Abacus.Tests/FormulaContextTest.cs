using System.Text;
using NUnit.Framework;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class FormulaContextTest
    {
        //TODO
        //[Test]
        //public void Should_be_able_to_determine_the_value_of_the_of_the_result_just_added()
        //{
        //    var methodContext = new FormulaContextOLD(string.Empty);

        //    Assert.AreEqual(100, methodContext.AddResult(new FormulaCalculationResult(new FormulaCalculation("first"), 100)).GetResult("first").Value);
        //    Assert.AreEqual(235, methodContext.AddResult(new FormulaCalculationResult(new FormulaCalculation("second"), 235)).GetResult("second").Value);
        //    Assert.AreEqual(12345.67m, methodContext.AddResult(new FormulaCalculationResult(new FormulaCalculation("third"), 12345.67m)).GetResult("third").Value);
        //}

        //[Test]
        //public void Should_be_able_to_use_a_logger()
        //{
        //    IExecutionContextLogger logger = new ExecutionContextLogger();

        //    var context = new FormulaContextOLD((string) "context");

        //    context.AddValue(new IntegerValueType("SomeInput", 10));

        //    var log = new StringBuilder();

        //    log.AppendLine("Argument answer: SomeInput = 10");

        //    Assert.AreEqual(log.ToString(), context.LogText);
        //}

        //[Test]
        //public void Should_keep_sub_totals()
        //{
        //    var methodContext = new FormulaContextOLD();

        //    var result1 = new CalculationCollectionResult(new CalculationCollection("first"), 100);

        //    methodContext.IncrementSubTotal(result1);
        //    methodContext.AddResult(result1);

        //    Assert.AreEqual(100, methodContext.GetSubTotal("first").Value);

        //    var result2 = new CalculationCollectionResult(new CalculationCollection("second"), 335);

        //    methodContext.IncrementSubTotal(result2);
        //    methodContext.AddResult(result2);

        //    Assert.AreEqual(435, methodContext.GetSubTotal("second").Value);

        //    var result3 = new CalculationCollectionResult(new CalculationCollection("third"), 12680.67m);

        //    methodContext.IncrementSubTotal(result3);
        //    methodContext.AddResult(result3);

        //    Assert.AreEqual(13115.67m, methodContext.GetSubTotal("third").Value);
        //}
    }
}
