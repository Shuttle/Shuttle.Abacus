using NUnit.Framework;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class FormulaFixture
    {
        //[Test]
        //public void Should_be_able_to_create_a_formula_with_an_initial_value()
        //{
        //    var formula = new Formula(new DecimalValueProvider(250));

        //    var methodContext = new FormulaContextOLD();

        //    Assert.AreEqual(250, formula.Execute(methodContext, new FormulaCalculationContext(methodContext)));
        //}

        //[Test]
        //public void Should_be_able_to_execute_a_simple_formula()
        //{
        //    var formula = new Formula();

        //    formula.AddOperation(new AdditionOperation(new DecimalValueProvider(10)));
        //    formula.AddOperation(new SubtractionOperation(new DecimalValueProvider(2)));
        //    formula.AddOperation(new MultiplicationOperation(new DecimalValueProvider(5)));
        //    formula.AddOperation(new DivisionOperation(new DecimalValueProvider(2)));

        //    var methodContext = new FormulaContextOLD();

        //    Assert.AreEqual(20, formula.Execute(methodContext, new FormulaCalculationContext(methodContext)));
        //}

        //[Test]
        //public void Should_be_able_to_execute_a_working_example()
        //{
        //    var formula = new Formula();

        //    formula.AddOperation(new AdditionOperation(new ArgumentAnswerValueProvider(new Argument
        //                                                                           {
        //                                                                               Name = "VoluntaryExcess"
        //                                                                           })));
        //    formula.AddOperation(new DivisionOperation(new ArgumentAnswerValueProvider(new Argument
        //                                                                           {
        //                                                                               Name = "SumInsured"
        //                                                                           })));
        //    formula.AddOperation(new SquareRootOperation(new RunningTotalValueProvider()));
        //    formula.AddOperation(new MultiplicationOperation(new DecimalValueProvider(-150)));
        //    formula.AddOperation(new RoundingOperation(new DecimalValueProvider(2)));
        //    formula.AddOperation(
        //        new MultiplicationOperation(new FormulaSubTotalValueProvider(new FormulaCalculation("TOTAL", true))));
        //    formula.AddOperation(new DivisionOperation(new DecimalValueProvider(100)));

        //    var context =
        //        new FormulaContextOLD("test")
        //            .AddValue(new DecimalDataType("VoluntaryExcess", 1000))
        //            .AddValue(new DecimalDataType("SumInsured", 980000));

        //    var result = new CalculationCollectionResult(new CalculationCollection("TOTAL"), 636.99m);

        //    context.IncrementSubTotal(result);
        //    context.AddResult(result);

        //    Assert.AreEqual(-30.51m, formula.Execute(context, new FormulaCalculationContext(context)).RoundToCents());
        //}
    }
}
