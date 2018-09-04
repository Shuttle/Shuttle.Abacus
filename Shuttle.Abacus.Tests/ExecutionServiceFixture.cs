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

            formula.AddOperation(Guid.NewGuid(), "Addition", "Argument", "Operand1");
            formula.AddOperation(Guid.NewGuid(), "Addition", "Argument", "Operand2");

            formulas.Add(formula);

            var operand1 = new Argument(Guid.NewGuid());

            operand1.Register("Operand1", "Integer");

            var operand2 = new Argument(Guid.NewGuid());

            operand2.Register("Operand2", "Decimal");

            var arguments = new List<Argument> { operand1, operand2 };

            var service = new ExecutionService(new ConstraintComparison(new DataTypeFactory()))
                .AddFormulaRange(formulas)
                .AddArgumentRange(arguments);

            var context = service.Execute("Test", new List<ArgumentValue>
            {
                new ArgumentValue (Guid.NewGuid(), "2"),
                new ArgumentValue (Guid.NewGuid(), "3")
            }, new ContextLogger(ContextLogLevel.Verbose));

            Assert.AreEqual(5, context.RootResult().Value);
        }

        [Test]
        public void Should_be_able_to_apply_simple_constraint()
        {
            var operand1 = new Argument(Guid.NewGuid());

            operand1.Register("Operand1", "Integer");

            var operand2 = new Argument(Guid.NewGuid());

            operand2.Register("Operand2", "Decimal");

            var formulas = new List<Formula>();
            var formula = new Formula(Guid.NewGuid());

            formula.Register("Test");

            formula.AddOperation(Guid.NewGuid(), "Addition", "Argument", "Operand1");
            formula.AddConstraint(Guid.NewGuid(), operand2.Id, "==", "10");

            formulas.Add(formula);

            var arguments = new List<Argument> { operand1, operand2 };

            var service = new ExecutionService(new ConstraintComparison(new DataTypeFactory()))
                .AddFormulaRange(formulas)
                .AddArgumentRange(arguments);

            var context = service.Execute("Test", new List<ArgumentValue>
            {
                new ArgumentValue (Guid.NewGuid(), "2"),
                new ArgumentValue (Guid.NewGuid(), "3")
            }, new ContextLogger(ContextLogLevel.Verbose));

            Assert.AreEqual(0, context.RootResult().Value);

            Console.WriteLine(context.Logger);
        }

        [Test]
        public void Should_be_able_to_use_formula_from_operation()
        {
            var formula1 = new Formula(Guid.NewGuid());

            formula1.Register("Formula1");
            formula1.AddOperation(Guid.NewGuid(), "Addition", "Formula", "Formula2");

            var formula2 = new Formula(Guid.NewGuid());

            formula2.Register("Formula2");
            formula2.AddOperation(Guid.NewGuid(), "Addition", "Decimal", "100");

            var formulas = new List<Formula> { formula1, formula2 };

            var service = new ExecutionService(new ConstraintComparison(new DataTypeFactory()))
                .AddFormulaRange(formulas);

            var context = service.Execute("Formula1", new List<ArgumentValue>(), new ContextLogger(ContextLogLevel.Verbose));

            Assert.AreEqual(100, context.Result());
        }

        [Test]
        public void Should_be_able_to_use_matrix()
        {
            var arguemtId = Guid.NewGuid();

            var formula = new Formula(Guid.NewGuid());

            formula.Register("Formula");
            formula.AddOperation(Guid.NewGuid(), "Addition", "Matrix", "simple-matrix");

            var matrix = new Matrix(Guid.NewGuid());

            matrix.Register("simple-matrix", arguemtId, null, "Decimal");
            matrix.RegisterConstraint(Guid.NewGuid(), "Row", 1, "==", "the-value");
            matrix.RegisterElement(Guid.NewGuid(), 1, 1, "1.25");

            var argument = new Argument(arguemtId);

            argument.Register("argument-one", "Text");

            var service = 
                new ExecutionService(new ConstraintComparison(new DataTypeFactory()))
                    .AddFormula(formula)
                    .AddArgument(argument)
                    .AddMatrix(matrix);

            var context = service.Execute("Formula", new List<ArgumentValue>
            {
                new ArgumentValue(Guid.NewGuid(), "the-value")
            }, new ContextLogger(ContextLogLevel.Verbose));

            Assert.AreEqual(1.25, context.Result());
        }

        [Test]
        public void Should_fail_on_cyclic_formulas()
        {
            var formula1 = new Formula(Guid.NewGuid());

            formula1.Register("Formula1");
            formula1.AddOperation(Guid.NewGuid(), "Addition", "Formula", "Formula2");

            var formula2 = new Formula(Guid.NewGuid());
            formula2.AddOperation(Guid.NewGuid(), "Addition", "Formula", "Formula3");

            formula2.Register("Formula2");

            var formula3 = new Formula(Guid.NewGuid());

            formula3.Register("Formula3");
            formula3.AddOperation(Guid.NewGuid(), "Addition", "Formula", "Formula4");

            var formula4 = new Formula(Guid.NewGuid());

            formula4.Register("Formula4");
            formula4.AddOperation(Guid.NewGuid(), "Addition", "Formula", "Formula5");

            var formula5 = new Formula(Guid.NewGuid());
            formula5.AddOperation(Guid.NewGuid(), "Addition", "Formula", "Formula1");

            formula5.Register("Formula5");

            var formulas = new List<Formula> { formula1, formula2, formula3, formula4, formula5 };

            var service = new ExecutionService(new ConstraintComparison(new DataTypeFactory()))
                .AddFormulaRange(formulas);

            var context = service.Execute("Formula1", new List<ArgumentValue>(), new ContextLogger(ContextLogLevel.Verbose));

            Assert.IsTrue(context.HasException);
            Assert.IsTrue(context.Exception.Message.Contains("Cyclic"));
        }
    }
}