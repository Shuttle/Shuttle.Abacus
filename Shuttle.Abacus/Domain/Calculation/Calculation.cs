using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public abstract class Calculation : Entity<Calculation>,
        ILimitOwner,
        IConstraintOwner,
        ISpecification<IMethodContext>
    {
        private readonly List<IConstraint> constraints = new List<IConstraint>();
        private readonly List<GraphNodeArgument> graphNodeArguments = new List<GraphNodeArgument>();
        private readonly List<Limit> limits = new List<Limit>();

        protected Calculation(string name, bool required)
        {
            Name = name;
            Required = required;
        }

        public string Name { get; private set; }
        public bool Required { get; private set; }
        public abstract string Type { get; }

        public Guid Id { get; protected set; }

        public IEnumerable<GraphNodeArgument> GraphNodeArguments
        {
            get { return new ReadOnlyCollection<GraphNodeArgument>(graphNodeArguments); }
        }

        public void AddGraphNodeArgument(Argument argument, string format)
        {
            graphNodeArguments.Add(new GraphNodeArgument(argument, format));
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

        ILimitOwner ILimitOwner.AddLimit(Limit limit)
        {
            Guard.AgainstNull(limit, "limit");

            Guard.Against<InvalidStateException>(
                limits.Find(item => item.Name.Equals(limit.Name, StringComparison.InvariantCultureIgnoreCase)) != null,
                string.Format(Localisation.Resources.DuplicateEntryException, limit.Name, "Limits"));

            limits.Add(limit);

            return this;
        }

        public IEnumerable<Limit> Limits
        {
            get { return new ReadOnlyCollection<Limit>(limits); }
        }

        public string OwnerName
        {
            get { return "Calculation"; }
        }

        public bool IsSatisfiedBy(IMethodContext methodContext)
        {
            Guard.AgainstNull(methodContext, "methodContext");

            foreach (var constraint in constraints)
            {
                if (!constraint.IsSatisfiedBy(methodContext))
                {
                    return false;
                }
            }

            return true;
        }

        public Calculation AddLimit(Limit limit)
        {
            limits.Add(limit);

            return this;
        }

        public void ApplyLimits(IMethodContext context, ICalculationResult value)
        {
            if (limits.Count == 0)
            {
                return;
            }

            var copycontext = context as ICanBeReadOnly<IMethodContext>;

            if (copycontext == null)
            {
                return;
            }

            var copy = context.Copy().AsReadOnly();

            foreach (var limit in limits)
            {
                limit.ApplyTo(value).Using(copy);

                if (!context.OK)
                {
                    return;
                }
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as Calculation;

            return other != null && Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public void AssignName(string name)
        {
            Name = name;
        }

        public Calculation ProcessCommand(
            SetCalculationConstraintsCommand command,
            IFactoryProvider<IConstraintFactory> constraintFactoryProvider,
            IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider,
            IMapper<ArgumentDTO, Argument> argumentDTOMapper)
        {
            constraints.Clear();

            command.Constraints.ForEach(
                constraint =>
                AddConstraint(
                    constraintFactoryProvider.Get(constraint.ConstraintTypeDTO.Name).Create(constraint.ArgumentDTO.Id,
                                                                                            argumentAnswerFactoryProvider.
                                                                                                Get(
                                                                                                constraint.ArgumentDTO.
                                                                                                    AnswerType).Create(
                                                                                                constraint.ArgumentDTO.
                                                                                                    Name,
                                                                                                constraint.Value))));

            return this;
        }

        public void ProcessCommand(ChangeCalculationCommand command)
        {
            Name = command.Name;
            Required = command.Required;
        }

        public abstract Calculation Copy(IDictionary<Guid, Guid> idMap);

        protected void Copy(Calculation calculation)
        {
            limits.ForEach(limit => calculation.AddLimit(limit.Copy()));
            constraints.ForEach(constraint => calculation.AddConstraint(constraint.Copy()));
        }

        public abstract ICalculationContext CalculationContext(IMethodContext methodContext);

        public abstract ICalculationResult Execute(IMethodContext methodContext,
                                                   ICalculationContext calculationContext);

        public void ClearGraphNodeArguments()
        {
            graphNodeArguments.Clear();
        }

        public void AddGraphNodeArgument(GraphNodeArgument graphNodeArgument)
        {
            Guard.AgainstNull(graphNodeArgument, "graphNodeArgument");

            graphNodeArguments.Add(graphNodeArgument);
        }
    }
}
