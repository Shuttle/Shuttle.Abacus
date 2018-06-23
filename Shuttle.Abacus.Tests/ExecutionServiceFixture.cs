using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class ExecutionServiceFixture
    {
        [Test]
        public void Should_be_able_to_perform_simple_addition()
        {
            var formulas = new List<Formula>();

            var formula = new Formula(Guid.NewGuid());

            formula.Register("Test");

            formula.AddOperation(TODO, "Addition", "Argument", "Operand1");
            formula.AddOperation(TODO, "Addition", "Argument", "Operand2");

            formulas.Add(formula);

            var operand1 = new Argument(Guid.NewGuid());

            operand1.Register("Operand1", "Integer");

            var operand2 = new Argument(Guid.NewGuid());

            operand2.Register("Operand2", "Decimal");

            var arguments = new List<Argument> { operand1, operand2 };

            var service = new ExecutionService(new ConstraintComparison(new ValueTypeFactory()), formulas, arguments, new List<Matrix>());

            var context = service.Execute("Test", new List<ArgumentValue>
            {
                new ArgumentValue ("Operand1", "2"),
                new ArgumentValue ("Operand2", "3")
            }, new ContextLogger(ContextLogLevel.Verbose));

            Assert.AreEqual(5, context.RootResult().Value);
        }

        [Test]
        public void Should_be_able_to_apply_simple_constraint()
        {
            var formulas = new List<Formula>();

            var formula = new Formula(Guid.NewGuid());

            formula.Register("Test");

            formula.AddOperation(TODO, "Addition", "Argument", "Operand1");
            formula.AddConstraint(TODO, "Operand2", "==", "10");

            formulas.Add(formula);

            var operand1 = new Argument(Guid.NewGuid());

            operand1.Register("Operand1", "Integer");

            var operand2 = new Argument(Guid.NewGuid());

            operand2.Register("Operand2", "Decimal");

            var arguments = new List<Argument> { operand1, operand2 };

            var service = new ExecutionService(new ConstraintComparison(new ValueTypeFactory()), formulas, arguments, new List<Matrix>());

            var context = service.Execute("Test", new List<ArgumentValue>
            {
                new ArgumentValue ("Operand1", "2"),
                new ArgumentValue ("Operand2", "3")
            }, new ContextLogger(ContextLogLevel.Verbose));

            Assert.AreEqual(0, context.RootResult().Value);

            Console.WriteLine(context.Logger);
        }

        [Test]
        public void Should_be_able_to_use_formula_from_operation()
        {
            var formula1 = new Formula(Guid.NewGuid());

            formula1.Register("Formula1");
            formula1.AddOperation(TODO, "Addition", "Formula", "Formula2");

            var formula2 = new Formula(Guid.NewGuid());

            formula2.Register("Formula2");
            formula2.AddOperation(TODO, "Addition", "Constant", "100");

            var formulas = new List<Formula> { formula1, formula2 };

            var service = new ExecutionService(new ConstraintComparison(new ValueTypeFactory()), formulas, new List<Argument>(), new List<Matrix>());

            var context = service.Execute("Formula1", new List<ArgumentValue>(), new ContextLogger(ContextLogLevel.Verbose));

            Assert.AreEqual(100, context.Result());
        }

        [Test]
        public void Should_be_able_to_use_matrix()
        {
            var formula = new Formula(Guid.NewGuid());

            formula.Register("Formula");
            formula.AddOperation(TODO, "Addition", "Matrix", "simple-matrix");

            var matrix = new Matrix(Guid.NewGuid());

            matrix.Register("simple-matrix", "argument-one", string.Empty, "Decimal");
            matrix.AddConstraint("Row", 1, "==", "the-value");
            matrix.AddElement(1, 1, "1.25");

            var argument = new Argument(Guid.NewGuid());

            argument.Register("argument-one", "Text");

            var service = new ExecutionService(
                new ConstraintComparison(new ValueTypeFactory()),
                new List<Formula> { formula },
                new List<Argument> { argument },
                new List<Matrix> { matrix });

            var context = service.Execute("Formula", new List<ArgumentValue>
            {
                new ArgumentValue("argument-one", "the-value")
            }, new ContextLogger(ContextLogLevel.Verbose));

            Assert.AreEqual(1.25, context.Result());
        }

        [Test]
        public void Should_fail_on_cyclic_formulas()
        {
            var formula1 = new Formula(Guid.NewGuid());

            formula1.Register("Formula1");
            formula1.AddOperation(TODO, "Addition", "Formula", "Formula2");

            var formula2 = new Formula(Guid.NewGuid());
            formula2.AddOperation(TODO, "Addition", "Formula", "Formula3");

            formula2.Register("Formula2");

            var formula3 = new Formula(Guid.NewGuid());

            formula3.Register("Formula3");
            formula3.AddOperation(TODO, "Addition", "Formula", "Formula4");

            var formula4 = new Formula(Guid.NewGuid());

            formula4.Register("Formula4");
            formula4.AddOperation(TODO, "Addition", "Formula", "Formula5");

            var formula5 = new Formula(Guid.NewGuid());
            formula5.AddOperation(TODO, "Addition", "Formula", "Formula1");

            formula5.Register("Formula5");

            var formulas = new List<Formula> { formula1, formula2, formula3, formula4, formula5 };

            var service = new ExecutionService(new ConstraintComparison(new ValueTypeFactory()), formulas, new List<Argument>(), new List<Matrix>());

            var context = service.Execute("Formula1", new List<ArgumentValue>(), new ContextLogger(ContextLogLevel.Verbose));

            Assert.IsTrue(context.HasException);
            Assert.IsTrue(context.Exception.Message.Contains("Cyclic"));
        }
    }
}