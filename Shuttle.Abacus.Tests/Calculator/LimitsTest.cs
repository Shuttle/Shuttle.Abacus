using NUnit.Framework;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class LimitsTest 
    {
        //[Test]
        //public void Should_be_able_to_limit_to_a_maximum_value()
        //{
        //    var calculation = new FormulaCalculation("calculation", true);

        //    calculation.AddFormula(new Formula().AddOperation(new AdditionOperation(new DecimalValueSource(200))));

        //    var limit = new MaximumLimit("max");

        //    limit.AddFormula(new Formula().AddOperation(new AdditionOperation(new DecimalValueSource(100))));

        //    calculation.AddLimit(limit);

        //    var methodContext = new FormulaContext();

        //    Assert.AreEqual(100, calculation.Execute(methodContext, new FormulaCalculationContext(methodContext)).Value);
        //}

        //[Test]
        //public void Should_be_able_to_limit_to_a_minimum_value()
        //{
        //    var calculation = new FormulaCalculation("calculation", true);

        //    calculation.AddFormula(new Formula().AddOperation(new AdditionOperation(new DecimalValueSource(100))));

        //    var limit = new MinimumLimit("min");

        //    limit.AddFormula(new Formula().AddOperation(new AdditionOperation(new DecimalValueSource(50))));

        //    calculation.AddLimit(limit);

        //    var methodContext = new FormulaContext();

        //    Assert.AreEqual(100, calculation.Execute(methodContext, new FormulaCalculationContext(methodContext)).Value);
        //}
    }
}
