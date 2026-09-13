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
            new Mock<IAlgorithmRepository>().Object,
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

        var algorithm = new Algorithm { Id = Guid.NewGuid() };

        algorithm.Register("Test");
        algorithm.RegisterOperation(Guid.NewGuid(), "Addition", "Argument", operand1.Id.ToString());
        algorithm.RegisterOperation(Guid.NewGuid(), "Addition", "Argument", operand2.Id.ToString());

        var service = GetExecutionService()
            .AddAlgorithmRange([algorithm])
            .AddArgumentRange(arguments);

        var context = await service.ExecuteAsync(algorithm.Id,
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

        var algorithm = new Algorithm { Id = Guid.NewGuid() };

        algorithm.Register("Test");
        algorithm.RegisterOperation(Guid.NewGuid(), "Addition", "Argument", operand1.Id.ToString());
        algorithm.RegisterConstraint(Guid.NewGuid(), operand2.Id, "==", "10");

        var arguments = new List<Argument> { operand1, operand2 };

        var service = GetExecutionService()
            .AddAlgorithmRange([algorithm])
            .AddArgumentRange(arguments);

        var context = await service.ExecuteAsync(algorithm.Id,
        [
            new(operand1.Id, "2"),
            new(operand2.Id, "3")
        ], new ContextLogger(ContextLogLevel.Verbose));

        Assert.That(context.RootResult()!.Value, Is.EqualTo(0));

        Console.WriteLine(context.Logger.ToString());
    }

    [Test]
    public async Task Should_be_able_to_use_algorithm_from_operation()
    {
        var algorithm2 = new Algorithm { Id = Guid.NewGuid() };

        algorithm2.Register("Algorithm2");
        algorithm2.RegisterOperation(Guid.NewGuid(), "Addition", "Decimal", "100");

        var algorithm1 = new Algorithm { Id = Guid.NewGuid() };

        algorithm1.Register("Algorithm1");
        algorithm1.RegisterOperation(Guid.NewGuid(), "Addition", "Algorithm", algorithm2.Id.ToString());

        var service = GetExecutionService().AddAlgorithmRange([algorithm1, algorithm2]);

        var context = await service.ExecuteAsync(algorithm1.Id, [], new ContextLogger(ContextLogLevel.Verbose));

        Assert.That(context.GetResult(), Is.EqualTo(100));

        Console.WriteLine(context.Logger.ToString());
    }

    [Test]
    public async Task Should_be_able_to_use_matrix()
    {
        var argumentId = Guid.NewGuid();

        var algorithm = new Algorithm { Id = Guid.NewGuid() };
        var matrix = new Matrix { Id = Guid.NewGuid() };

        matrix.Register("simple-matrix", argumentId, null, "Decimal");
        matrix.RegisterConstraint(Guid.NewGuid(), "Row", 1, "==", "the-value");
        matrix.RegisterElement(Guid.NewGuid(), 1, 1, "1.25");

        algorithm.Register("Algorithm");
        algorithm.RegisterOperation(Guid.NewGuid(), "Addition", "Matrix", matrix.Id.ToString());

        var argument = new Argument { Id = argumentId };

        argument.Register("argument-one", "Text");

        var service = GetExecutionService()
            .AddAlgorithm(algorithm)
            .AddArgument(argument)
            .AddMatrix(matrix);

        var context = await service.ExecuteAsync(algorithm.Id,
        [
            new(argumentId, "the-value")
        ], new ContextLogger(ContextLogLevel.Verbose));

        Assert.That(context.GetResult(), Is.EqualTo(1.25m));

        Console.WriteLine(context.Logger.ToString());
    }

    [Test]
    public async Task Should_fail_on_cyclic_algorithms()
    {
        var algorithm1 = new Algorithm { Id = Guid.NewGuid() };
        var algorithm5 = new Algorithm { Id = Guid.NewGuid() };

        algorithm5.RegisterOperation(Guid.NewGuid(), "Addition", "Algorithm", algorithm1.Id.ToString());
        algorithm5.Register("Algorithm5");

        var algorithm4 = new Algorithm { Id = Guid.NewGuid() };

        algorithm4.Register("Algorithm4");
        algorithm4.RegisterOperation(Guid.NewGuid(), "Addition", "Algorithm", algorithm5.Id.ToString());

        var algorithm3 = new Algorithm { Id = Guid.NewGuid() };

        algorithm3.Register("Algorithm3");
        algorithm3.RegisterOperation(Guid.NewGuid(), "Addition", "Algorithm", algorithm4.Id.ToString());

        var algorithm2 = new Algorithm { Id = Guid.NewGuid() };

        algorithm2.RegisterOperation(Guid.NewGuid(), "Addition", "Algorithm", algorithm3.Id.ToString());
        algorithm2.Register("Algorithm2");

        algorithm1.Register("Algorithm1");
        algorithm1.RegisterOperation(Guid.NewGuid(), "Addition", "Algorithm", algorithm2.Id.ToString());

        var service = GetExecutionService().AddAlgorithmRange([algorithm1, algorithm2, algorithm3, algorithm4, algorithm5]);

        var context = await service.ExecuteAsync(algorithm1.Id, [], new ContextLogger(ContextLogLevel.Verbose));

        Assert.That(context.HasException, Is.True);
        Assert.That(context.Exception!.Message, Does.Contain("Cyclic"));

        Console.WriteLine(context.Logger.ToString());
    }
}
