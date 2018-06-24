using NUnit.Framework;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class CompleteCalculationTest
    {
        //private static Calculation AreaFactor()
        //{
        //    const string inputParameter = "Area";

        //    var result = new Calculation("Area", true);

        //    var source = new ResultValueProvider("BasePremium");

        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(85.100000m)))).For(inputParameter)
        //        .WithValue(0);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(85.100000m)))).For(inputParameter)
        //        .WithValue(0);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(80.500000m)))).For(inputParameter)
        //        .WithValue(1);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(76.000000m)))).For(inputParameter)
        //        .WithValue(2);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(71.400000m)))).For(inputParameter)
        //        .WithValue(3);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(66.800000m)))).For(inputParameter)
        //        .WithValue(4);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(63.000000m)))).For(inputParameter)
        //        .WithValue(5);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(59.100000m)))).For(inputParameter)
        //        .WithValue(6);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(54.400000m)))).For(inputParameter)
        //        .WithValue(7);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(51.500000m)))).For(inputParameter)
        //        .WithValue(8);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(48.400000m)))).For(inputParameter)
        //        .WithValue(9);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(45.400000m)))).For(inputParameter)
        //        .WithValue(10);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(42.200000m)))).For(inputParameter)
        //        .WithValue(11);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(39.900000m)))).For(inputParameter)
        //        .WithValue(12);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(39.200000m)))).For(inputParameter)
        //        .WithValue(13);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(37.600000m)))).For(inputParameter)
        //        .WithValue(14);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(36.100000m)))).For(inputParameter)
        //        .WithValue(15);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(34.300000m)))).For(inputParameter)
        //        .WithValue(16);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(32.600000m)))).For(inputParameter)
        //        .WithValue(17);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(30.700000m)))).For(inputParameter)
        //        .WithValue(18);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(28.700000m)))).For(inputParameter)
        //        .WithValue(19);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(26.800000m)))).For(inputParameter)
        //        .WithValue(20);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(24.900000m)))).For(inputParameter)
        //        .WithValue(21);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(23.000000m)))).For(inputParameter)
        //        .WithValue(22);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(21.100000m)))).For(inputParameter)
        //        .WithValue(23);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(19.100000m)))).For(inputParameter)
        //        .WithValue(24);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(17.600000m)))).For(inputParameter)
        //        .WithValue(25);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(15.700000m)))).For(inputParameter)
        //        .WithValue(26);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(13.800000m)))).For(inputParameter)
        //        .WithValue(27);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(11.900000m)))).For(inputParameter)
        //        .WithValue(28);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(10.000000m)))).For(inputParameter)
        //        .WithValue(29);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(8.300000m)))).For(inputParameter)
        //        .WithValue(30);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(6.500000m)))).For(inputParameter)
        //        .WithValue(31);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(4.100000m)))).For(inputParameter)
        //        .WithValue(32);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(2.200000m)))).For(inputParameter)
        //        .WithValue(33);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(0.200000m)))).For(inputParameter)
        //        .WithValue(34);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-1.000000m)))).For(inputParameter)
        //        .WithValue(35);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-2.800000m)))).For(inputParameter)
        //        .WithValue(36);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-4.800000m)))).For(inputParameter)
        //        .WithValue(37);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-6.700000m)))).For(inputParameter)
        //        .WithValue(38);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-8.600000m)))).For(inputParameter)
        //        .WithValue(39);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-10.200000m)))).For(
        //        inputParameter, 40);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-12.200000m)))).For(
        //        inputParameter, 41);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-14.000000m)))).For(
        //        inputParameter, 42);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-15.900000m)))).For(
        //        inputParameter, 43);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-17.800000m)))).For(
        //        inputParameter, 44);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-19.400000m)))).For(
        //        inputParameter, 45);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-21.300000m)))).For(
        //        inputParameter, 46);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-23.300000m)))).For(
        //        inputParameter, 47);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-25.200000m)))).For(
        //        inputParameter, 48);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-27.100000m)))).For(
        //        inputParameter, 49);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-28.700000m)))).For(
        //        inputParameter, 50);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-30.600000m)))).For(
        //        inputParameter, 51);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-32.500000m)))).For(
        //        inputParameter, 52);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-34.400000m)))).For(
        //        inputParameter, 53);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-36.300000m)))).For(
        //        inputParameter, 54);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-38.000000m)))).For(
        //        inputParameter, 55);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-39.900000m)))).For(
        //        inputParameter, 56);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-41.800000m)))).For(
        //        inputParameter, 57);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-43.700000m)))).For(
        //        inputParameter, 58);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-45.600000m)))).For(
        //        inputParameter, 59);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-47.200000m)))).For(
        //        inputParameter, 60);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-49.100000m)))).For(
        //        inputParameter, 61);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-51.000000m)))).For(
        //        inputParameter, 62);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-53.000000m)))).For(
        //        inputParameter, 63);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-54.900000m)))).For(
        //        inputParameter, 64);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-56.500000m)))).For(
        //        inputParameter, 65);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-58.700000m)))).For(
        //        inputParameter, 66);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-60.800000m)))).For(
        //        inputParameter, 67);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-62.800000m)))).For(
        //        inputParameter, 68);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-64.600000m)))).For(
        //        inputParameter, 69);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-65.800000m)))).For(
        //        inputParameter, 70);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-67.700000m)))).For(
        //        inputParameter, 71);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-69.600000m)))).For(
        //        inputParameter, 72);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-71.400000m)))).For(
        //        inputParameter, 73);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-74.400000m)))).For(
        //        inputParameter, 74);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-75.000000m)))).For(
        //        inputParameter, 75);

        //    return result;
        //}

        //private static Calculation BasePremium()
        //{
        //    const string inputParameter = "SumInsured";

        //    var result = new Calculation("BasePremium", true);

        //    var source = new InputValueProvider("SumInsured");

        //    result.AddFormula(
        //        new Formula()
        //            .AddOperation(new AdditionOperation(new FormulaValueProvider(69.77m)))).To(inputParameter, 60000);

        //    result.AddFormula(
        //        new Formula(source)
        //            .AddOperation(new SubtractionOperation(new FormulaValueProvider(60000)))
        //            .AddOperation(new PercentageOperation(new FormulaValueProvider(0.0623m)))
        //            .AddOperation(new AdditionOperation(new FormulaValueProvider(69.77m)))
        //        ).To(inputParameter, 150000);

        //    result.AddFormula(
        //        new Formula(source)
        //            .AddOperation(new SubtractionOperation(new FormulaValueProvider(150000)))
        //            .AddOperation(new PercentageOperation(new FormulaValueProvider(0.0500m)))
        //            .AddOperation(new AdditionOperation(new FormulaValueProvider(125.84m)))
        //        ).To(inputParameter, 270000);

        //    result.AddFormula(
        //        new Formula(source)
        //            .AddOperation(new SubtractionOperation(new FormulaValueProvider(270000)))
        //            .AddOperation(new PercentageOperation(new FormulaValueProvider(0.0400m)))
        //            .AddOperation(new AdditionOperation(new FormulaValueProvider(176.84m)))
        //        ).To(inputParameter, 400000);

        //    result.AddFormula(
        //        new Formula(source)
        //            .AddOperation(new SubtractionOperation(new FormulaValueProvider(400000)))
        //            .AddOperation(new PercentageOperation(new FormulaValueProvider(0.0300m)))
        //            .AddOperation(new AdditionOperation(new FormulaValueProvider(221.04m)))
        //        ).To(inputParameter, 700000);

        //    result.AddFormula(
        //        new Formula(source)
        //            .AddOperation(new SubtractionOperation(new FormulaValueProvider(700000)))
        //            .AddOperation(new PercentageOperation(new FormulaValueProvider(0.0200m)))
        //            .AddOperation(new AdditionOperation(new FormulaValueProvider(297.54m)))
        //        ).From(inputParameter, 700000);

        //    return result;
        //}

        //private static Calculation RoofConstruction()
        //{
        //    const string inputParameter = "RoofConstruction";

        //    var result = new Calculation("RoofConstruction", true);

        //    var source = new InputValueProvider("SumInsured");

        //    result.AddFormula(new Formula(source).AddOperation(new AdditionOperation(source))).For(inputParameter, 0);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new MultiplicationOperation(new FormulaValueProvider(0.00075m)))).For(
        //        inputParameter, 1);
        //    result.AddFormula(
        //        new Formula(source).AddOperation(new MultiplicationOperation(new FormulaValueProvider(0.00068m)))).For(
        //        inputParameter, 2);

        //    return result;
        //}

        //private static Calculation Age()
        //{
        //    var result = new Calculation("Age", true);

        //    const string inputParameter = "Age";

        //    var source = new SubTotalValueProvider("WallConstruction");

        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(25)))).
        //        From(inputParameter, 18).To(inputParameter, 20);

        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(0)))).To(
        //        inputParameter, 34);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-10)))).To
        //        (inputParameter, 44);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-20)))).To
        //        (inputParameter, 54);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-35)))).
        //        From(inputParameter, 55);

        //    return result;
        //}

        //private static Calculation HolidayHome()
        //{
        //    var result = new Calculation("HolidayHome", false);

        //    result.AddFormula(
        //        new Formula(new ResultValueProvider("BasePremium")).AddOperation(
        //            new PercentageOperation(new FormulaValueProvider(50)))).For("HolidayHome", 1);

        //    return result;
        //}

        //private static Calculation WallConstruction()
        //{
        //    var result = new Calculation("WallConstruction", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueProvider("Area")).AddOperation(
        //            new PercentageOperation(new FormulaValueProvider(14)))).For("WallConstruction", 1);

        //    return result;
        //}

        //private static Calculation NCB()
        //{
        //    var result = new Calculation("NCB", true);

        //    const string inputParameter = "NCB";

        //    var source = new SubTotalValueProvider("Age");

        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(33)))).For
        //        (inputParameter, 0);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(26.7m)))).
        //        For(inputParameter, 1);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(20)))).For
        //        (inputParameter, 2);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(13.3m)))).
        //        For(inputParameter, 3);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(6.7m)))).
        //        For(inputParameter, 4);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(0)))).For(
        //        inputParameter, 5);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-5)))).For
        //        (inputParameter, 6);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-10)))).
        //        For(inputParameter, 7);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(-10)))).
        //        For(inputParameter, 8);

        //    return result;
        //}

        //private static Calculation CoverType()
        //{
        //    const string inputParameter = "CoverType";

        //    var result = new Calculation("CoverType", true);

        //    var source = new SubTotalValueProvider("NCB");

        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(0)))).For(
        //        inputParameter, 0);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(30)))).For
        //        (inputParameter, 1);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(25)))).For
        //        (inputParameter, 2);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(60)))).For
        //        (inputParameter, 3);

        //    return result;
        //}

        //private static Calculation TheftBurglarySuspended()
        //{
        //    var result = new Calculation("TheftBurglarySuspended", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueProvider("CoverType")).AddOperation(
        //            new PercentageOperation(new FormulaValueProvider(-20)))).For("TheftBurglarySuspended", 1);

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
        //        new Formula(new SubTotalValueProvider("VoluntaryExcess")).AddOperation(
        //            new PercentageOperation(new FormulaValueProvider(25))));

        //    calculation.AddLimit(new MaximumLimit(limit));

        //    return calculation;
        //}

        //private static Calculation BurglarBars()
        //{
        //    var result = new Calculation("BurglarBars", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueProvider("MonitoredAlarm")).AddOperation(
        //            new PercentageOperation(new FormulaValueProvider(-5)))).For("BurglarBars", 1);

        //    return result;
        //}

        //private static Calculation BurglarBarsAndGates()
        //{
        //    var result = new Calculation("BurglarBarsAndGates", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueProvider("BurglarBars")).AddOperation(
        //            new PercentageOperation(new FormulaValueProvider(-5)))).For("BurglarBarsAndGates", 1);

        //    return result;
        //}

        //private static Calculation ControlledEntrance()
        //{
        //    var result = new Calculation("ControlledEntrance", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueProvider("BurglarBarsAndGates")).AddOperation(
        //            new PercentageOperation(new FormulaValueProvider(-25)))).For("ControlledEntrance", 1);

        //    return result;
        //}

        //private static Calculation MonitoredAlarm()
        //{
        //    var result = new Calculation("MonitoredAlarm", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueProvider("VoluntaryExcess")).AddOperation(
        //            new PercentageOperation(new FormulaValueProvider(-15)))).For("MonitoredAlarm", 1);

        //    return result;
        //}

        //private static Calculation VoluntaryExcess()
        //{
        //    var result = new Calculation("VoluntaryExcess", true);

        //    result.AddFormula(new Formula()
        //                          .AddOperation(new AdditionOperation(new InputValueProvider("VoluntaryExcess")))
        //                          .AddOperation(new DivisionOperation(new InputValueProvider("SumInsured")))
        //                          .AddOperation(new SquareRootOperation())
        //                          .AddOperation(new MultiplicationOperation(new FormulaValueProvider(-150)))
        //                          .AddOperation(new RoundingOperation(2))
        //                          .AddOperation(new MultiplicationOperation(new SubTotalValueProvider("FloodCoverExcluded")))
        //                          .AddOperation(new DivisionOperation(new FormulaValueProvider(100))));

        //    return result;
        //}

        //private static Calculation FloodCoverExcluded()
        //{
        //    var result = new Calculation("FloodCoverExcluded", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueProvider("TheftBurglarySuspended")).AddOperation(
        //            new PercentageOperation(new FormulaValueProvider(-10)))).For("FloodCoverExcluded", 1);

        //    return result;
        //}

        //private static Calculation VRI()
        //{
        //    var result = new Calculation("VRI", false);

        //    result.AddFormula(new Formula().AddOperation(new AdditionOperation(new FormulaValueProvider(38)))).For("VRI").
        //        WithValue(1);

        //    return result;
        //}

        //private static Calculation AllRisks()
        //{
        //    var result = new Calculation("AllRisks", false);

        //    result.AddFormula(
        //        new Formula(new SubTotalValueProvider("Area")).AddOperation(
        //            new PercentageOperation(new FormulaValueProvider(50)))).For("AllRisks", 1);

        //    return result;
        //}

        //private static Calculation Breakdown()
        //{
        //    var result = new Calculation("Breakdown", false);

        //    result.AddFormula(new Formula().AddOperation(new AdditionOperation(new FormulaValueProvider(2)))).For(
        //        "Breakdown", 1);

        //    return result;
        //}

        //private static Calculation DiscretionaryFactor()
        //{
        //    var result = new Calculation("DiscretionaryFactor", false);

        //    result.AddFormula(new Formula(new SubTotalValueProvider("MultiClaimantLoading"))
        //                          .AddOperation(new MultiplicationOperation(new InputValueProvider("DiscretionaryFactor")))
        //                          .AddOperation(new DivisionOperation(new FormulaValueProvider(100))));

        //    return result;
        //}

        //private static Calculation MultiClaimantLoading()
        //{
        //    const string inputParameter = "MultiClaimantLoading";

        //    var result = new Calculation("MultiClaimantLoading", true);

        //    var source = new SubTotalValueProvider("SecurityDiscounts");

        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(0)))).For(
        //        inputParameter, 0);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(5)))).For(
        //        inputParameter, 1);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(10)))).For
        //        (inputParameter, 2);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(15)))).For
        //        (inputParameter, 3);
        //    result.AddFormula(new Formula(source).AddOperation(new PercentageOperation(new FormulaValueProvider(20)))).For
        //        (inputParameter, 4);

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
