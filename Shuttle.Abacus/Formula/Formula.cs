/*
    This file forms part of Shuttle.Abacus.

    Shuttle.Abacus - A constraint-based calculation engine.
    Copyright (C) 2016  Eben Roux

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class Formula :
        ISpecification<IMethodContext>,
        IConstraintOwner
    {
        private readonly List<IConstraint> constraints = new List<IConstraint>();
        private readonly List<FormulaOperation> operations = new List<FormulaOperation>();

        public Formula()
        {
        }

        public Formula(IValueSource initialValueSource)
            : this()
        {
            Guard.AgainstNull(initialValueSource, "initialValueSource");

            operations.Add(new AdditionOperation(initialValueSource));
        }

        public Formula(
            ICreateFormulaCommand command,
            IFactoryProvider<IOperationFactory> operationFactoryProvider,
            IFactoryProvider<IValueSourceFactory> valueSourceFactoryProvider,
            IFactoryProvider<IConstraintFactory> constraintFactoryProvider,
            IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider)
            : this()
        {
            command.Operations.ForEach(
                operation =>
                AddOperation(
                    operationFactoryProvider.Get(operation.OperationType.Name).Create(
                        valueSourceFactoryProvider.Get(operation.ValueSourceType.Name).Create(operation.ValueSelection))));

            command.Constraints.ForEach(
                constraint =>
                AddConstraint(
                    constraintFactoryProvider.Get(constraint.ConstraintTypeDTO.Name).Create(constraint.ArgumentDto.Id, argumentAnswerFactoryProvider.Get(constraint.ArgumentDto.AnswerType).Create(constraint.ArgumentDto.Name, constraint.Value))));
        }

        public Formula(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public bool HasOperations
        {
            get { return operations.Count > 0; }
        }

        public IEnumerable<FormulaOperation> Operations
        {
            get { return new ReadOnlyCollection<FormulaOperation>(operations); }
        }

        public IEnumerable<IConstraint> Constraints
        {
            get { return new ReadOnlyCollection<IConstraint>(constraints); }
        }

        public IConstraintOwner AddConstraint(IConstraint constraint)
        {
            Guard.AgainstNull(constraint, "constraint");

            constraints.Add(constraint);

            return this;
        }

        public string OwnerName
        {
            get { return "Formula"; }
        }

        public IEnumerable<Guid> RequiredCalculationIds()
        {
            var result = new List<Guid>();

            operations.ForEach(operation =>
                {
                    var source = operation.ValueSource as ICalculationValueSource;

                    if (source != null)
                    {
                        var id = new Guid(source.ValueSelection);

                        if (!result.Contains(id))
                        {
                            result.Add(id);
                        }
                    }
                });

            return result;
        }

        public bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            return
                OperationsSatisfied(collectionMethodContext)
                &&
                ConstraintSatisfied(collectionMethodContext);
        }

        private bool ConstraintSatisfied(IMethodContext collectionContext)
        {
            foreach (var constraint in constraints)
            {
                if (!constraint.IsSatisfiedBy(collectionContext))
                {
                    return false;
                }
            }

            return true;
        }

        private bool OperationsSatisfied(IMethodContext collectionContext)
        {
            var result = true;

            foreach (var operation in operations)
            {
                if (!operation.IsSatisfiedBy(collectionContext))
                {
                    result = false;
                }
            }

            return result;
        }

        public Formula ProcessCommand(
            IChangeFormulaCommand command,
            IFactoryProvider<IOperationFactory> operationFactoryProvider,
            IFactoryProvider<IValueSourceFactory> valueSourceFactoryProvider,
            IFactoryProvider<IConstraintFactory> constraintFactoryProvider,
            IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider,
            IMapper<ArgumentDTO, Argument> argumentDTOMapper)
        {
            operations.Clear();

            command.Operations.ForEach(
                operation =>
                AddOperation(
                    operationFactoryProvider.Get(operation.OperationType.Name).Create(
                        valueSourceFactoryProvider.Get(operation.ValueSourceType.Name)
                            .Create(operation.ValueSelection))));

            constraints.Clear();

            command.Constraints.ForEach(
                constraint =>
                AddConstraint(
                    constraintFactoryProvider.Get(constraint.ConstraintTypeDTO.Name)
                        .Create(constraint.ArgumentDto.Id, argumentAnswerFactoryProvider.Get(constraint.ArgumentDto.AnswerType).Create(constraint.ArgumentDto.Name, constraint.Value))));

            return this;
        }

        public string Description()
        {
            if (constraints.Count == 0)
            {
                return "(unconstrained)";
            }

            var result = new StringBuilder();

            foreach (var constraint in constraints)
            {
                result.AppendFormat("{0}{1}", result.Length > 0
                                                  ? " and "
                                                  : string.Empty, constraint.Description());
            }

            return result.ToString();
        }

        public Formula AddOperation(FormulaOperation operation)
        {
            operations.Add(operation);

            return this;
        }

        public decimal Execute(IMethodContext methodContext, IFormulaCalculationContext calculationContext)
        {
            Guard.AgainstNull(calculationContext, "context");

            if (methodContext.LoggerEnabled)
            {
                methodContext.Log("Executing formula:");

                if(constraints.Count>0)
                {
                    constraints.ForEach(
                        constraint => methodContext.Log("\t{0}", constraint.Description()));
                }
                else
                {
                    methodContext.Log("\t(no contraints)");
                }

                methodContext.Log();
            }

            calculationContext.ZeroFormulaTotal();

            foreach (var operation in operations)
            {
                var operand = operation.Operand(methodContext, calculationContext);

                if (methodContext.LoggerEnabled)
                {
                    methodContext.Log("{0}\t{1}", operation.Symbol, operation.ValueSource.Description(operand, methodContext));
                }

                calculationContext.SetFormulaTotal(operation.Execute(calculationContext.FormulaTotal, operand));

                if (!methodContext.OK)
                {
                    return 0;
                }
            }

            if (methodContext.LoggerEnabled)
            {
                methodContext.Log();
            }

            return calculationContext.FormulaTotal;
        }

        public Formula Copy()
        {
            var result = new Formula();

            constraints.ForEach(constraint => result.AddConstraint(constraint.Copy()));
            operations.ForEach(operation => result.AddOperation(operation.Copy()));

            return result;
        }
    }
}
