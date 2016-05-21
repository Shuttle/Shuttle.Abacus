using System.Linq;
using NUnit.Framework;
using Shuttle.Abacus;

namespace Abacus.Test.Unit
{
    [TestFixture]
    public class CalculationTest
    {
        [Test]
        public void Should_be_able_to_get_a_list_of_required_sub_total_names()
        {
            var calculation = new FormulaCalculation("test", false);

            var list = calculation.RequiredCalculationIds();

            Assert.AreEqual(0, list.Count());

            var collection = new CalculationCollection((string) "subtotal1");

            calculation.AddFormula(new Formula(new CalculationSubTotalValueSource(collection)));

            list = calculation.RequiredCalculationIds();

            Assert.AreEqual(1, list.Count());
            Assert.AreEqual(collection.Id, list.First());
        }
    }
}
