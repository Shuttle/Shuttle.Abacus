using System;
using System.Collections.Generic;
using Moq;
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

            var operand1 = new Argument(Guid.NewGuid());

            operand1.Register("Operand1", "Integer");

            var operand2 = new Argument(Guid.NewGuid());

            operand2.Register("Operand2", "Decimal");

            var arguments = new List<Argument> { operand1, operand2 };

            var formula = new Formula(Guid.NewGuid());

            formula.Register("Test");

            formula.AddOperation(Guid.NewGuid(), "Addition", "Argument", operand1.Id.ToString());
            formula.AddOperation(Guid.NewGuid(), "Addition", "Argument", operand2.Id.ToString());

            formulas.Add(formula);

            var service = GetExecutionService()
                .AddFormulaRange(formulas)
                .AddArgumentRange(arguments);

            var context = service.Execute(formula.Id, new List<ArgumentValue>
            {
                new ArgumentValue (operand1.Id, "2"),
                new ArgumentValue (operand2.Id, "3")
            }, new ContextLogger(ContextLogLevel.Verbose));

            Assert.AreEqual(5, context.RootResult().Value);
        }

        private IExecutionService GetExecutionService()
        {
            return new ExecutionService(
                new ConstraintComparison(new DataTypeFactory()),
                new Mock<IFormulaRepository>().Object,
                new Mock<IArgumentRepository>().Object,
                new Mock<IMatrixRepository>().Object);
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

            formula.AddOperation(Guid.NewGuid(), "Addition", "Argument", operand1.Id.ToString());
            formula.AddConstraint(Guid.NewGuid(), operand2.Id, "==", "10");

            formulas.Add(formula);

            var arguments = new List<Argument> { operand1, operand2 };

            var service = GetExecutionService()
                .AddFormulaRange(formulas)
                .AddArgumentRange(arguments);

            var context = service.Execute(formula.Id, new List<ArgumentValue>
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
            var formula2 = new Formula(Guid.NewGuid());

            formula2.Register("Formula2");
            formula2.AddOperation(Guid.NewGuid(), "Addition", "Decimal", "100");

            var formula1 = new Formula(Guid.NewGuid());

            formula1.Register("Formula1");
            formula1.AddOperation(Guid.NewGuid(), "Addition", "Formula", formula2.Id.ToString());

            var formulas = new List<Formula> { formula1, formula2 };

            var service = GetExecutionService()
                .AddFormulaRange(formulas);

            var context = service.Execute(formula1.Id, new List<ArgumentValue>(), new ContextLogger(ContextLogLevel.Verbose));

            Assert.AreEqual(100, context.Result());
        }

        [Test]
        public void Should_be_able_to_use_matrix()
        {
            var argumentId = Guid.NewGuid();

            var formula = new Formula(Guid.NewGuid());

            var matrix = new Matrix(Guid.NewGuid());

            matrix.Register("simple-matrix", argumentId, null, "Decimal");
            matrix.RegisterConstraint(Guid.NewGuid(), "Row", 1, "==", "the-value");
            matrix.RegisterElement(Guid.NewGuid(), 1, 1, "1.25");

            formula.Register("Formula");
            formula.AddOperation(Guid.NewGuid(), "Addition", "Matrix", matrix.Id.ToString());

            var argument = new Argument(argumentId);

            argument.Register("argument-one", "Text");

            var service =
                GetExecutionService()
                    .AddFormula(formula)
                    .AddArgument(argument)
                    .AddMatrix(matrix);

            var context = service.Execute(formula.Id, new List<ArgumentValue>
            {
                new ArgumentValue(argumentId, "the-value")
            }, new ContextLogger(ContextLogLevel.Verbose));

            Assert.AreEqual(1.25, context.Result());
        }

        [Test]
        public void Should_fail_on_cyclic_formulas()
        {
            var formula1 = new Formula(Guid.NewGuid());

            var formula5 = new Formula(Guid.NewGuid());

            formula5.AddOperation(Guid.NewGuid(), "Addition", "Formula", formula1.Id.ToString());
            formula5.Register("Formula5");

            var formula4 = new Formula(Guid.NewGuid());

            formula4.Register("Formula4");
            formula4.AddOperation(Guid.NewGuid(), "Addition", "Formula", formula5.Id.ToString());

            var formula3 = new Formula(Guid.NewGuid());

            formula3.Register("Formula3");
            formula3.AddOperation(Guid.NewGuid(), "Addition", "Formula", formula4.Id.ToString());

            var formula2 = new Formula(Guid.NewGuid());

            formula2.AddOperation(Guid.NewGuid(), "Addition", "Formula", formula3.Id.ToString());
            formula2.Register("Formula2");

            formula1.Register("Formula1");
            formula1.AddOperation(Guid.NewGuid(), "Addition", "Formula", formula2.Id.ToString());

            var formulas = new List<Formula> { formula1, formula2, formula3, formula4, formula5 };

            var service = GetExecutionService()
                .AddFormulaRange(formulas);

            var context = service.Execute(formula1.Id, new List<ArgumentValue>(), new ContextLogger(ContextLogLevel.Verbose));

            Assert.IsTrue(context.HasException);
            Assert.IsTrue(context.Exception.Message.Contains("Cyclic"));
        }
    }
}