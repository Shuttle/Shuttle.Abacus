using NUnit.Framework;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class CompleteCalculationTest
    {
        //private static Calculation AreaFactor()
        //{
        //    const string input = "Area";

        //    var result = new Calculation("Area", true);

        //    var source = new ResultValueSource("BasePremium");

        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(85.100000m)))).For(input)
        //        .WithValue(0);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(85.100000m)))).For(input)
        //        .WithValue(0);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(80.500000m)))).For(input)
        //        .WithValue(1);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(76.000000m)))).For(input)
        //        .WithValue(2);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(71.400000m)))).For(input)
        //        .WithValue(3);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(66.800000m)))).For(input)
        //        .WithValue(4);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(63.000000m)))).For(input)
        //        .WithValue(5);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(59.100000m)))).For(input)
        //        .WithValue(6);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(54.400000m)))).For(input)
        //        .WithValue(7);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(51.500000m)))).For(input)
        //        .WithValue(8);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(48.400000m)))).For(input)
        //        .WithValue(9);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(45.400000m)))).For(input)
        //        .WithValue(10);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(42.200000m)))).For(input)
        //        .WithValue(11);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(39.900000m)))).For(input)
        //        .WithValue(12);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(39.200000m)))).For(input)
        //        .WithValue(13);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(37.600000m)))).For(input)
        //        .WithValue(14);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(36.100000m)))).For(input)
        //        .WithValue(15);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(34.300000m)))).For(input)
        //        .WithValue(16);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(32.600000m)))).For(input)
        //        .WithValue(17);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(30.700000m)))).For(input)
        //        .WithValue(18);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(28.700000m)))).For(input)
        //        .WithValue(19);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(26.800000m)))).For(input)
        //        .WithValue(20);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(24.900000m)))).For(input)
        //        .WithValue(21);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(23.000000m)))).For(input)
        //        .WithValue(22);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(21.100000m)))).For(input)
        //        .WithValue(23);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(19.100000m)))).For(input)
        //        .WithValue(24);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(17.600000m)))).For(input)
        //        .WithValue(25);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(15.700000m)))).For(input)
        //        .WithValue(26);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(13.800000m)))).For(input)
        //        .WithValue(27);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(11.900000m)))).For(input)
        //        .WithValue(28);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(10.000000m)))).For(input)
        //        .WithValue(29);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(8.300000m)))).For(input)
        //        .WithValue(30);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(6.500000m)))).For(input)
        //        .WithValue(31);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(4.100000m)))).For(input)
        //        .WithValue(32);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(2.200000m)))).For(input)
        //        .WithValue(33);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(0.200000m)))).For(input)
        //        .WithValue(34);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-1.000000m)))).For(input)
        //        .WithValue(35);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-2.800000m)))).For(input)
        //        .WithValue(36);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-4.800000m)))).For(input)
        //        .WithValue(37);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-6.700000m)))).For(input)
        //        .WithValue(38);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-8.600000m)))).For(input)
        //        .WithValue(39);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-10.200000m)))).For(
        //        input, 40);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-12.200000m)))).For(
        //        input, 41);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-14.000000m)))).For(
        //        input, 42);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-15.900000m)))).For(
        //        input, 43);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-17.800000m)))).For(
        //        input, 44);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-19.400000m)))).For(
        //        input, 45);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-21.300000m)))).For(
        //        input, 46);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-23.300000m)))).For(
        //        input, 47);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-25.200000m)))).For(
        //        input, 48);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-27.100000m)))).For(
        //        input, 49);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-28.700000m)))).For(
        //        input, 50);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-30.600000m)))).For(
        //        input, 51);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-32.500000m)))).For(
        //        input, 52);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-34.400000m)))).For(
        //        input, 53);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-36.300000m)))).For(
        //        input, 54);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-38.000000m)))).For(
        //        input, 55);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-39.900000m)))).For(
        //        input, 56);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-41.800000m)))).For(
        //        input, 57);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-43.700000m)))).For(
        //        input, 58);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-45.600000m)))).For(
        //        input, 59);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-47.200000m)))).For(
        //        input, 60);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-49.100000m)))).For(
        //        input, 61);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-51.000000m)))).For(
        //        input, 62);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-53.000000m)))).For(
        //        input, 63);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-54.900000m)))).For(
        //        input, 64);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-56.500000m)))).For(
        //        input, 65);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-58.700000m)))).For(
        //        input, 66);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-60.800000m)))).For(
        //        input, 67);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-62.800000m)))).For(
        //        input, 68);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-64.600000m)))).For(
        //        input, 69);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-65.800000m)))).For(
        //        input, 70);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-67.700000m)))).For(
        //        input, 71);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-69.600000m)))).For(
        //        input, 72);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-71.400000m)))).For(
        //        input, 73);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-74.400000m)))).For(
        //        input, 74);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-75.000000m)))).For(
        //        input, 75);

        //    return result;
        //}

        //private static Calculation BasePremium()
        //{
        //    const string input = "SumInsured";

        //    var result = new Calculation("BasePremium", true);

        //    var source = new InputValueSource("SumInsured");

        //    result.AddFormula(
        //        new Formula()
        //            .AddOperation(new AdditionOperation(new FormulaValueSource(69.77m)))).To(input, 60000);

        //    result.AddFormula(
        //        new Formula(source)
        //            .AddOperation(new SubtractionOperation(new FormulaValueSource(60000)))
        //            .AddOperation(new PercentageOperation(new FormulaValueSource(0.0623m)))
        //            .AddOperation(new AdditionOperation(new FormulaValueSource(69.77m)))
        //        ).To(input, 150000);

        //    result.AddFormula(
        //        new Formula(source)
        //            .AddOperation(new SubtractionOperation(new FormulaValueSource(150000)))
        //            .AddOperation(new PercentageOperation(new FormulaValueSource(0.0500m)))
        //            .AddOperation(new AdditionOperation(new FormulaValueSource(125.84m)))
        //        ).To(input, 270000);

        //    result.AddFormula(
        //        new Formula(source)
        //            .AddOperation(new SubtractionOperation(new FormulaValueSource(270000)))
        //            .AddOperation(new PercentageOperation(new FormulaValueSource(0.0400m)))
        //            .AddOperation(new AdditionOperation(new FormulaValueSource(176.84m)))
        //        ).To(input, 400000);

        //    result.AddFormula(
        //        new Formula(source)
        //            .AddOperation(new SubtractionOperation(new FormulaValueSource(400000)))
        //            .AddOperation(new PercentageOperation(new FormulaValueSource(0.0300m)))
        //            .AddOperation(new AdditionOperation(new FormulaValueSource(221.04m)))
        //        ).To(input, 700000);

        //    result.AddFormula(
        //        new Formula(source)
        //            .AddOperation(new SubtractionOperation(new FormulaValueSource(700000)))
        //            .AddOperation(new PercentageOperation(new FormulaValueSource(0.0200m)))
        //            .AddOperation(new AdditionOperation(new FormulaValueSource(297.54m)))
        //        ).From(input, 700000);

        //    return result;
        //}

        //private static Calculation RoofConstruction()
        //{
        //    const string input = "RoofConstruction";

        //    var result = new Calculation("RoofConstruction", true);

        //    var source = new InputValueSource("SumInsured");

        //    result.AddFormula(new Formula(source).AddOperation(new AdditionOperation(source))).For(input, 0);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new MultiplicationOperation(new FormulaValueSource(0.00075m)))).For(
        //        input, 1);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new MultiplicationOperation(new FormulaValueSource(0.00068m)))).For(
        //        input, 2);

        //    return result;
        //}

        //private static Calculation Age()
        //{
        //    var result = new Calculation("Age", true);

        //    const string input = "Age";

        //    var source = new SubTotalValueSource("WallConstruction");

        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(25)))).
        //        From(input, 18).To(input, 20);

        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(0)))).To(
        //        input, 34);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-10)))).To
        //        (input, 44);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-20)))).To
        //        (input, 54);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-35)))).
        //        From(input, 55);

        //    return result;
        //}

        //private static Calculation HolidayHome()
        //{
        //    var result = new Calculation("HolidayHome", false);

        //    result.AddFormula(
        //        new Formula(new ResultValueSource("BasePremium")).AddOperation(
        //            new PercentageOperation(new FormulaValueSource(50)))).For("HolidayHome", 1);

        //    return result;
        //}

        //private static Calculation WallConstruction()
        //{
        //    var result = new Calculation("WallConstruction", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueSource("Area")).AddOperation(
        //            new PercentageOperation(new FormulaValueSource(14)))).For("WallConstruction", 1);

        //    return result;
        //}

        //private static Calculation NCB()
        //{
        //    var result = new Calculation("NCB", true);

        //    const string input = "NCB";

        //    var source = new SubTotalValueSource("Age");

        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(33)))).For
        //        (input, 0);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(26.7m)))).
        //        For(input, 1);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(20)))).For
        //        (input, 2);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(13.3m)))).
        //        For(input, 3);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(6.7m)))).
        //        For(input, 4);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(0)))).For(
        //        input, 5);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-5)))).For
        //        (input, 6);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-10)))).
        //        For(input, 7);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(-10)))).
        //        For(input, 8);

        //    return result;
        //}

        //private static Calculation CoverType()
        //{
        //    const string input = "CoverType";

        //    var result = new Calculation("CoverType", true);

        //    var source = new SubTotalValueSource("NCB");

        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(0)))).For(
        //        input, 0);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(30)))).For
        //        (input, 1);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(25)))).For
        //        (input, 2);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(60)))).For
        //        (input, 3);

        //    return result;
        //}

        //private static Calculation TheftBurglarySuspended()
        //{
        //    var result = new Calculation("TheftBurglarySuspended", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueSource("CoverType")).AddOperation(
        //            new PercentageOperation(new FormulaValueSource(-20)))).For("TheftBurglarySuspended", 1);

        //    return result;
        //}

        //private static AbstractCalculation SecurityDiscounts()
        //{
        //    var calculation = new CalculationCollection("SecurityDiscounts");

        //    calculation.AddCalculation(MonitoredAlarm());

        //    calculation.AddCalculation(BurglarBars());

        //    calculation.AddCalculation(BurglarBarsAndGates());

        //    calculation.AddCalculation(ControlledEntrance());

        //    var limit = new Calculation("SecurityDiscountLimit", false);

        //    limit.AddFormula(
        //        new Formula(new SubTotalValueSource("VoluntaryExcess")).AddOperation(
        //            new PercentageOperation(new FormulaValueSource(25))));

        //    calculation.AddLimit(new MaximumLimit(limit));

        //    return calculation;
        //}

        //private static Calculation BurglarBars()
        //{
        //    var result = new Calculation("BurglarBars", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueSource("MonitoredAlarm")).AddOperation(
        //            new PercentageOperation(new FormulaValueSource(-5)))).For("BurglarBars", 1);

        //    return result;
        //}

        //private static Calculation BurglarBarsAndGates()
        //{
        //    var result = new Calculation("BurglarBarsAndGates", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueSource("BurglarBars")).AddOperation(
        //            new PercentageOperation(new FormulaValueSource(-5)))).For("BurglarBarsAndGates", 1);

        //    return result;
        //}

        //private static Calculation ControlledEntrance()
        //{
        //    var result = new Calculation("ControlledEntrance", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueSource("BurglarBarsAndGates")).AddOperation(
        //            new PercentageOperation(new FormulaValueSource(-25)))).For("ControlledEntrance", 1);

        //    return result;
        //}

        //private static Calculation MonitoredAlarm()
        //{
        //    var result = new Calculation("MonitoredAlarm", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueSource("VoluntaryExcess")).AddOperation(
        //            new PercentageOperation(new FormulaValueSource(-15)))).For("MonitoredAlarm", 1);

        //    return result;
        //}

        //private static Calculation VoluntaryExcess()
        //{
        //    var result = new Calculation("VoluntaryExcess", true);

        //    result.AddFormula(new Formula()
        //                          .AddOperation(new AdditionOperation(new InputValueSource("VoluntaryExcess")))
        //                          .AddOperation(new DivisionOperation(new InputValueSource("SumInsured")))
        //                          .AddOperation(new SquareRootOperation())
        //                          .AddOperation(new MultiplicationOperation(new FormulaValueSource(-150)))
        //                          .AddOperation(new RoundingOperation(2))
        //                          .AddOperation(new MultiplicationOperation(new SubTotalValueSource("FloodCoverExcluded")))
        //                          .AddOperation(new DivisionOperation(new FormulaValueSource(100))));

        //    return result;
        //}

        //private static Calculation FloodCoverExcluded()
        //{
        //    var result = new Calculation("FloodCoverExcluded", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueSource("TheftBurglarySuspended")).AddOperation(
        //            new PercentageOperation(new FormulaValueSource(-10)))).For("FloodCoverExcluded", 1);

        //    return result;
        //}

        //private static Calculation VRI()
        //{
        //    var result = new Calculation("VRI", false);

        //    result.AddFormula(new Formula().AddOperation(new AdditionOperation(new FormulaValueSource(38)))).For("VRI").
        //        WithValue(1);

        //    return result;
        //}

        //private static Calculation AllRisks()
        //{
        //    var result = new Calculation("AllRisks", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueSource("Area")).AddOperation(
        //            new PercentageOperation(new FormulaValueSource(50)))).For("AllRisks", 1);

        //    return result;
        //}

        //private static Calculation Breakdown()
        //{
        //    var result = new Calculation("Breakdown", false);

        //    result.AddFormula(new Formula().AddOperation(new AdditionOperation(new FormulaValueSource(2)))).For(
        //        "Breakdown", 1);

        //    return result;
        //}

        //private static Calculation DiscretionaryFactor()
        //{
        //    var result = new Calculation("DiscretionaryFactor", false);

        //    result.AddFormula(new Formula(new SubTotalValueSource("MultiClaimantLoading"))
        //                          .AddOperation(new MultiplicationOperation(new InputValueSource("DiscretionaryFactor")))
        //                          .AddOperation(new DivisionOperation(new FormulaValueSource(100))));

        //    return result;
        //}

        //private static Calculation MultiClaimantLoading()
        //{
        //    const string input = "MultiClaimantLoading";

        //    var result = new Calculation("MultiClaimantLoading", true);

        //    var source = new SubTotalValueSource("SecurityDiscounts");

        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(0)))).For(
        //        input, 0);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(5)))).For(
        //        input, 1);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(10)))).For
        //        (input, 2);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(15)))).For
        //        (input, 3);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueSource(20)))).For
        //        (input, 4);

        //    return result;
        //}

        //[Test]
        //public void Should_be_able_to_calculate_a_complete_home_content_premium()
        //{
        //    var calculation = new CalculationCollection("Contents");


        //    var basePremium = BasePremium();

        //    calculation.AddCalculation(basePremium);


        //    var areaFactor = AreaFactor();

        //    areaFactor.DependsOn(basePremium);

        //    calculation.AddCalculation(areaFactor);


        //    var roofConstruction = RoofConstruction();

        //    roofConstruction.DependsOn(areaFactor);

        //    calculation.AddCalculation(roofConstruction);


        //    var wallConstruction = WallConstruction();

        //    wallConstruction.DependsOn(roofConstruction);

        //    calculation.AddCalculation(wallConstruction);


        //    var holidayHome = HolidayHome();

        //    holidayHome.DependsOn(wallConstruction);

        //    calculation.AddCalculation(holidayHome);


        //    var age = Age();

        //    age.DependsOn(holidayHome);

        //    calculation.AddCalculation(age);


        //    var ncb = NCB();

        //    ncb.DependsOn(age);

        //    calculation.AddCalculation(ncb);


        //    var coverType = CoverType();

        //    coverType.DependsOn(ncb);

        //    calculation.AddCalculation(coverType);


        //    var theftBurglarySuspended = TheftBurglarySuspended();

        //    theftBurglarySuspended.DependsOn(coverType);

        //    calculation.AddCalculation(theftBurglarySuspended);


        //    var floodCoverExcluded = FloodCoverExcluded();

        //    floodCoverExcluded.DependsOn(theftBurglarySuspended);

        //    calculation.AddCalculation(floodCoverExcluded);


        //    var voluntaryExcess = VoluntaryExcess();

        //    voluntaryExcess.DependsOn(floodCoverExcluded);

        //    calculation.AddCalculation(voluntaryExcess);


        //    var securityDiscounts = SecurityDiscounts();

        //    securityDiscounts.DependsOn(voluntaryExcess);

        //    calculation.AddCalculation(securityDiscounts);


        //    var multiClaimantLoading = MultiClaimantLoading();

        //    multiClaimantLoading.DependsOn(securityDiscounts);

        //    calculation.AddCalculation(multiClaimantLoading);


        //    var discretionaryFactor = DiscretionaryFactor();

        //    discretionaryFactor.DependsOn(multiClaimantLoading);

        //    calculation.AddCalculation(discretionaryFactor);


        //    var breakdown = Breakdown();

        //    breakdown.DependsOn(discretionaryFactor);

        //    calculation.AddCalculation(breakdown);


        //    var allRisks = AllRisks();

        //    allRisks.DependsOn(breakdown);

        //    calculation.AddCalculation(allRisks);


        //    var vri = VRI();

        //    vri.DependsOn(allRisks);

        //    calculation.AddCalculation(vri);

        //    // setup complete --- execute

        //    //var sw = new Stopwatch();

        //    //sw.Start();

        //    //int i;

        //    //for (i = 0; i < 100000; i++)
        //    //{
        //        var context = new CalculationContext(new CalculationTextLogger());

        //        context.AddInput("SumInsured", 980000);
        //        context.AddInput("Area", 26);
        //        context.AddInput("RoofConstruction", 1); // 0=non-standard, 1=thatch, 2=thatchsayf
        //        context.AddInput("WallConstruction", 1); // 0=standard, 1=non-standard
        //        context.AddInput("HolidayHome", 0); // 0=false, 1=true
        //        context.AddInput("Age", 59);
        //        context.AddInput("NCB", 3);
        //        context.AddInput("CoverType", 0); // 0=standard, 1=all risk, 2=excluding theft, 3=limited
        //        context.AddInput("TheftBurglarySuspended", 1); // 0=false, 1=true
        //        context.AddInput("FloodCoverExcluded", 1); // 0=false, 1=true
        //        context.AddInput("VoluntaryExcess", 1000);
        //        context.AddInput("MonitoredAlarm", 1); // 0=false, 1=true
        //        context.AddInput("BurglarBars", 0); // 0=false, 1=true
        //        context.AddInput("BurglarBarsAndGates", 0); // 0=false, 1=true
        //        context.AddInput("ControlledEntrance", 0); // 0=false, 1=true
        //        context.AddInput("MultiClaimantLoading", 2);
        //        context.AddInput("DiscretionaryFactor", -10);
        //        context.AddInput("Breakdown", 1); // 0=false, 1=true
        //        context.AddInput("AllRisks", 1); // 0=false, 1=true
        //        context.AddInput("VRI", 1); // 0=false, 1=true

        //        var result = calculation.Execute(context);

        //        Console.WriteLine(context.Logger.ToString());

        //        if (!result.OK)
        //        {
        //            Console.WriteLine(result.ToString());
        //        }

        //        Assert.AreEqual(754.87m, result.Value);
        //    //}

        //    //sw.Stop();

        //    //Console.WriteLine("{0} took {1} ms", i, sw.ElapsedMilliseconds);
        //}
    }
}
