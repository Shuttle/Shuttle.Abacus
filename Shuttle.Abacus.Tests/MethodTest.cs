using NUnit.Framework;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class MethodTest
    {
        //[Test]
        //public void Should_be_able_to_apply_limits()
        //{
        //    var method = new Formula { MethodName = "method" };

        //    var formula = new FormulaCalculation("formula", true);

        //    formula
        //        .AddFormula(
        //        new Formula()
        //            .AddOperation(
        //            new AdditionOperation(
        //                new ConstantValueSource(100))));

        //    method.AddCalculation(formula);

        //    var context = new FormulaContext();

        //    method.Calculate(context);

        //    Assert.AreEqual(100, context.Total.Value);

        //    var limit = new MinimumLimit("minimum");

        //    limit.AddFormula(new Formula()
        //                         .AddOperation(
        //                         new AdditionOperation(
        //                             new ConstantValueSource(200))));

        //    method.AddLimit(limit);

        //    context = new FormulaContext();

        //    method.Calculate(context);

        //    Assert.AreEqual(200, context.Total.Value);
        //}

        //[Test]
        //public void Should_be_able_to_calculate_sub_calculations()
        //{
        //    var method = new Formula
        //                  {
        //                      MethodName = "Formula"
        //                  };

        //    var level1 = new CalculationCollection((string) "level1");
        //    var level2 = new CalculationCollection((string) "level2");
        //    var level3 = new CalculationCollection((string) "level3");

        //    level1.AddCalculation(level2);
        //    level2.AddCalculation(level3);

        //    var formula1 = new FormulaCalculation("formula1", false);

        //    var formula = new Formula();

        //    formula.AddOperation(new AdditionOperation(new ConstantValueSource(100)));

        //    formula1.AddFormula(formula);

        //    level3.AddCalculation(formula1);

        //    method.AddCalculation(level1);

        //    var context = new FormulaContext();

        //    method.Calculate(context);

        //    Assert.AreEqual(100, context.Total.Value);
        //}

        //[Test]
        //public void Should_be_able_to_create_a_copy_of_a_method()
        //{
        //    var method = new Formula();

        //    method.AddCalculation(new FormulaCalculation("one", true));
        //    method.AddCalculation(new FormulaCalculation("two", true));
        //    method.AddCalculation(new FormulaCalculation("three", true));

        //    method.Copy();
        //}

        //[Test]
        //public void Should_not_be_able_to_add_calculations_that_contain_formulas_using_subtotals_not_yet_registered()
        //{
        //    var method = new Formula();

        //    var basepremium = new FormulaCalculation("basepremium", true);

        //    basepremium.AddFormula(
        //        new Formula().AddOperation(new AdditionOperation(new ArgumentAnswerValueSource(new Argument
        //                                                                                     {
        //                                                                                         Name = "SumInsured"
        //                                                                                     }))));

        //    method.AddCalculation(basepremium);

        //    var usesbasepremium = new FormulaCalculation("usesbasepremium", true);

        //    usesbasepremium.AddFormula(
        //        new Formula().AddOperation(new AdditionOperation(new FormulaSubTotalValueSource(basepremium))));

        //    method.AddCalculation(usesbasepremium);

        //    var usesbogus = new FormulaCalculation("usesbogus", true);

        //    usesbogus.AddFormula(
        //        new Formula().AddOperation(
        //            new AdditionOperation(new FormulaSubTotalValueSource(new FormulaCalculation("bogus", true)))));

        //    method.AddCalculation(usesbogus);

        //    Assert.Throws<InvalidCalculationOrderException>(method.EnforceInvariants);
        //}
    }
}
