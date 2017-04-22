using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class Formula : Core.Infrastructure.ISpecification<IMethodContext>,
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
            CreateFormulaCommand command,
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
                            valueSourceFactoryProvider.Get(operation.ValueSourceType.Name)
                                .Create(operation.ValueSelection))));

            command.Constraints.ForEach(
                constraint =>
                    AddConstraint(
                        constraintFactoryProvider.Get(constraint.ConstraintTypeDTO.Name)
                            .Create(constraint.ArgumentDTO.Id,
                                argumentAnswerFactoryProvider.Get(constraint.ArgumentDTO.AnswerType)
                                    .Create(constraint.ArgumentDTO.Name, constraint.Value))));
        }

        public Formula(Guid id)
        {
            Id = id;
        }

        public bool HasOperations
        {
            get { return operations.Count > 0; }
        }

        public IEnumerable<FormulaOperation> Operations
        {
            get { return new ReadOnlyCollection<FormulaOperation>(operations); }
        }

        public Guid Id { get; }

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

        public bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            return
                OperationsSatisfied(collectionMethodContext)
                &&
                ConstraintSatisfied(collectionMethodContext);
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
            ChangeFormulaCommand command,
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
                            .Create(constraint.ArgumentDTO.Id,
                                argumentAnswerFactoryProvider.Get(constraint.ArgumentDTO.AnswerType)
                                    .Create(constraint.ArgumentDTO.Name, constraint.Value))));

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

                if (constraints.Count > 0)
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
                    methodContext.Log("{0}\t{1}", operation.Symbol,
                        operation.ValueSource.Description(operand, methodContext));
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