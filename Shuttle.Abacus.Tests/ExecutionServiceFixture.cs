using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shuttle.Abacus.Domain;

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

            formula.AddOperation(1, "Addition", "Argument", "Operand1");
            formula.AddOperation(1, "Addition", "Argument", "Operand2");

            formulas.Add(formula);

            var operand1 = new Argument(Guid.NewGuid());

            operand1.Register("Operand1", "Integer");

            var operand2 = new Argument(Guid.NewGuid());

            operand2.Register("Operand2", "Decimal");

            var arguments = new List<Argument> { operand1, operand2 };

            var service = new ExecutionService(new ConstraintComparison(new ValueTypeFactory()), formulas, arguments);

            var context = service.Execute("Test", new List<ArgumentValue>
            {
                new ArgumentValue {Name = "Operand1", Value = "2"},
                new ArgumentValue {Name = "Operand2", Value = "3"}
            });

            Assert.AreEqual(5, context.RootResult().Value);
        }

        [Test]
        public void Should_be_able_to_apply_simple_constraint()
        {
            var formulas = new List<Formula>();

            var formula = new Formula(Guid.NewGuid());

            formula.Register("Test");

            formula.AddOperation(1, "Addition", "Argument", "Operand1");
            formula.AddConstraint(1, "Operand2", "==", "10");

            formulas.Add(formula);

            var operand1 = new Argument(Guid.NewGuid());

            operand1.Register("Operand1", "Integer");

            var operand2 = new Argument(Guid.NewGuid());

            operand2.Register("Operand2", "Decimal");

            var arguments = new List<Argument> { operand1, operand2 };

            var service = new ExecutionService(new ConstraintComparison(new ValueTypeFactory()), formulas, arguments);

            var context = service.Execute("Test", new List<ArgumentValue>
            {
                new ArgumentValue {Name = "Operand1", Value = "2"},
                new ArgumentValue {Name = "Operand2", Value = "3"}
            });

            Assert.AreEqual(0, context.RootResult().Value);
        }

        [Test]
        public void Should_be_able_to_use_formula_from_operation()
        {
            var formula1 = new Formula(Guid.NewGuid());

            formula1.Register("Formula1");
            formula1.AddOperation(1, "Addition", "Formula", "Formula2");

            var formula2 = new Formula(Guid.NewGuid());

            formula2.Register("Formula2");
            formula2.AddOperation(1, "Addition", "Constant", "100");

            var formulas = new List<Formula> { formula1, formula2 };

            var service = new ExecutionService(new ConstraintComparison(new ValueTypeFactory()), formulas, new List<Argument>());

            var context = service.Execute("Formula1", new List<ArgumentValue>());

            Assert.AreEqual(100, context.Result());
        }

        [Test]
        public void Should_fail_on_cyclic_formulas()
        {
            var formula1 = new Formula(Guid.NewGuid());

            formula1.Register("Formula1");
            formula1.AddOperation(1, "Addition", "Formula", "Formula2");

            var formula2 = new Formula(Guid.NewGuid());
            formula2.AddOperation(1, "Addition", "Formula", "Formula3");

            formula2.Register("Formula2");

            var formula3 = new Formula(Guid.NewGuid());

            formula3.Register("Formula3");
            formula3.AddOperation(1, "Addition", "Formula", "Formula4");

            var formula4 = new Formula(Guid.NewGuid());

            formula4.Register("Formula4");
            formula4.AddOperation(1, "Addition", "Formula", "Formula5");

            var formula5 = new Formula(Guid.NewGuid());
            formula5.AddOperation(1, "Addition", "Formula", "Formula1");

            formula5.Register("Formula5");

            var formulas = new List<Formula> {formula1, formula2, formula3, formula4, formula5};

            var service = new ExecutionService(new ConstraintComparison(new ValueTypeFactory()), formulas, new List<Argument>());

            var context = service.Execute("Formula1", new List<ArgumentValue>());

            Assert.IsTrue(context.HasException);
            Assert.IsTrue(context.Exception.Message.Contains("Cyclic"));
        }
    }
}