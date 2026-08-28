using Moq;
using NUnit.Framework;

namespace Shuttle.Abacus.Tests;

[TestFixture]
public class ExecutionServiceFixture
{
    private static IExecutionService GetExecutionService()
    {
        return new ExecutionService(
            new ValueComparer(new DataTypeFactory()),
            new Mock<IFormulaRepository>().Object,
            new Mock<IArgumentRepository>().Object,
            new Mock<IMatrixRepository>().Object);
    }

    [Test]
    public async Task Should_be_able_to_perform_simple_addition()
    {
        var operand1 = new Argument { Id = Guid.NewGuid() };

        operand1.Register("Operand1", "Integer");

        var operand2 = new Argument { Id = Guid.NewGuid() };

        operand2.Register("Operand2", "Decimal");

        var arguments = new List<Argument> { operand1, operand2 };

        var formula = new Formula { Id = Guid.NewGuid() };

        formula.Register("Test");
        formula.RegisterOperation(Guid.NewGuid(), "Addition", "Argument", operand1.Id.ToString());
        formula.RegisterOperation(Guid.NewGuid(), "Addition", "Argument", operand2.Id.ToString());

        var service = GetExecutionService()
            .AddFormulaRange([formula])
            .AddArgumentRange(arguments);

        var context = await service.ExecuteAsync(formula.Id,
        [
            new(operand1.Id, "2"),
            new(operand2.Id, "3")
        ], new ContextLogger(ContextLogLevel.Verbose));

        Assert.That(context.RootResult()!.Value, Is.EqualTo(5));

        Console.WriteLine(context.Logger.ToString());
    }

    [Test]
    public async Task Should_be_able_to_apply_simple_constraint()
    {
        var operand1 = new Argument { Id = Guid.NewGuid() };

        operand1.Register("Operand1", "Integer");

        var operand2 = new Argument { Id = Guid.NewGuid() };

        operand2.Register("Operand2", "Decimal");

        var formula = new Formula { Id = Guid.NewGuid() };

        formula.Register("Test");
        formula.RegisterOperation(Guid.NewGuid(), "Addition", "Argument", operand1.Id.ToString());
        formula.RegisterConstraint(Guid.NewGuid(), operand2.Id, "==", "10");

        var arguments = new List<Argument> { operand1, operand2 };

        var service = GetExecutionService()
            .AddFormulaRange([formula])
            .AddArgumentRange(arguments);

        var context = await service.ExecuteAsync(formula.Id,
        [
            new(operand1.Id, "2"),
            new(operand2.Id, "3")
        ], new ContextLogger(ContextLogLevel.Verbose));

        Assert.That(context.RootResult()!.Value, Is.EqualTo(0));

        Console.WriteLine(context.Logger.ToString());
    }

    [Test]
    public async Task Should_be_able_to_use_formula_from_operation()
    {
        var formula2 = new Formula { Id = Guid.NewGuid() };

        formula2.Register("Formula2");
        formula2.RegisterOperation(Guid.NewGuid(), "Addition", "Decimal", "100");

        var formula1 = new Formula { Id = Guid.NewGuid() };

        formula1.Register("Formula1");
        formula1.RegisterOperation(Guid.NewGuid(), "Addition", "Formula", formula2.Id.ToString());

        var service = GetExecutionService().AddFormulaRange([formula1, formula2]);

        var context = await service.ExecuteAsync(formula1.Id, [], new ContextLogger(ContextLogLevel.Verbose));

        Assert.That(context.GetResult(), Is.EqualTo(100));

        Console.WriteLine(context.Logger.ToString());
    }

    [Test]
    public async Task Should_be_able_to_use_matrix()
    {
        var argumentId = Guid.NewGuid();

        var formula = new Formula { Id = Guid.NewGuid() };
        var matrix = new Matrix { Id = Guid.NewGuid() };

        matrix.Register("simple-matrix", argumentId, null, "Decimal");
        matrix.RegisterConstraint(Guid.NewGuid(), "Row", 1, "==", "the-value");
        matrix.RegisterElement(Guid.NewGuid(), 1, 1, "1.25");

        formula.Register("Formula");
        formula.RegisterOperation(Guid.NewGuid(), "Addition", "Matrix", matrix.Id.ToString());

        var argument = new Argument { Id = argumentId };

        argument.Register("argument-one", "Text");

        var service = GetExecutionService()
            .AddFormula(formula)
            .AddArgument(argument)
            .AddMatrix(matrix);

        var context = await service.ExecuteAsync(formula.Id,
        [
            new(argumentId, "the-value")
        ], new ContextLogger(ContextLogLevel.Verbose));

        Assert.That(context.GetResult(), Is.EqualTo(1.25m));

        Console.WriteLine(context.Logger.ToString());
    }

    [Test]
    public async Task Should_fail_on_cyclic_formulas()
    {
        var formula1 = new Formula { Id = Guid.NewGuid() };
        var formula5 = new Formula { Id = Guid.NewGuid() };

        formula5.RegisterOperation(Guid.NewGuid(), "Addition", "Formula", formula1.Id.ToString());
        formula5.Register("Formula5");

        var formula4 = new Formula { Id = Guid.NewGuid() };

        formula4.Register("Formula4");
        formula4.RegisterOperation(Guid.NewGuid(), "Addition", "Formula", formula5.Id.ToString());

        var formula3 = new Formula { Id = Guid.NewGuid() };

        formula3.Register("Formula3");
        formula3.RegisterOperation(Guid.NewGuid(), "Addition", "Formula", formula4.Id.ToString());

        var formula2 = new Formula { Id = Guid.NewGuid() };

        formula2.RegisterOperation(Guid.NewGuid(), "Addition", "Formula", formula3.Id.ToString());
        formula2.Register("Formula2");

        formula1.Register("Formula1");
        formula1.RegisterOperation(Guid.NewGuid(), "Addition", "Formula", formula2.Id.ToString());

        var service = GetExecutionService().AddFormulaRange([formula1, formula2, formula3, formula4, formula5]);

        var context = await service.ExecuteAsync(formula1.Id, [], new ContextLogger(ContextLogLevel.Verbose));

        Assert.That(context.HasException, Is.True);
        Assert.That(context.Exception!.Message, Does.Contain("Cyclic"));

        Console.WriteLine(context.Logger.ToString());
    }
}
