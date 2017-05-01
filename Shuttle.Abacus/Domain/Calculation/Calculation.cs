using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public abstract class Calculation :
        ILimitOwner,
        IConstraintOwner,
        ISpecification<IMethodContext>
    {
        private readonly List<IConstraint> constraints = new List<IConstraint>();
        private readonly List<GraphNodeArgument> graphNodeArguments = new List<GraphNodeArgument>();
        private readonly List<Limit> limits = new List<Limit>();

        private readonly List<OwnedConstraint> _constraints = new List<OwnedConstraint>();
        private readonly List<OwnedLimit> _limits = new List<OwnedLimit>();

        protected Calculation(Guid id, string name, bool required)
        {
            Id = id;
            Name = name;
            Required = required;
        }

        public Guid Id { get; protected set; }
        public string Name { get; private set; }
        public bool Required { get; private set; }
        public abstract string Type { get; }

        public IEnumerable<GraphNodeArgument> GraphNodeArguments
        {
            get { return new ReadOnlyCollection<GraphNodeArgument>(graphNodeArguments); }
        }

        public IEnumerable<OwnedConstraint> Constraints
        {
            get { return new ReadOnlyCollection<OwnedConstraint>(_constraints); }
        }

        public IConstraintOwner AddConstraint(IConstraint constraint)
        {
            Guard.AgainstNull(constraint, "constraint");

            constraints.Add(constraint);

            return this;
        }

        public void AddConstraint(OwnedConstraint item)
        {
            Guard.AgainstNull(item, "item");

            _constraints.Add(item);
        }

        public ILimitOwner AddLimit(Limit limit)
        {
            Guard.AgainstNull(limit, "limit");

            Guard.Against<InvalidStateException>(
                limits.Find(item => item.Name.Equals(limit.Name, StringComparison.InvariantCultureIgnoreCase)) != null,
                string.Format(Resources.DuplicateEntryException, limit.Name, "Limits"));

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

        public void AddGraphNodeArgument(Argument argument, string format)
        {
            graphNodeArguments.Add(new GraphNodeArgument(argument, format));
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

        public Calculation ProcessCommand(SetCalculationConstraintsCommand command)
        {
            constraints.Clear();

            throw new NotImplementedException();

            //command.Constraints.ForEach(
            //    constraint =>
            //        AddConstraint(
            //            constraintFactoryProvider.Get(constraint.ConstraintTypeDTO.Name)
            //                .Create(constraint.DataRow.Id,
            //                    argumentAnswerFactoryProvider.
            //                        Get(
            //                            constraint.DataRow.
            //                                AnswerType).Create(
            //                            constraint.DataRow.
            //                                Name,
            //                            constraint.Value))));

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

        public void AddLimit(OwnedLimit limit)
        {
            Guard.AgainstNull(limit, "limit");

            _limits.Add(limit);
        }
    }
}