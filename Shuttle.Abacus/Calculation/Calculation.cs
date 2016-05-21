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
using Shuttle.Abacus.Localisation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public abstract class Calculation :
        ISpecification<IMethodContext>,
        ILimitOwner,
        IConstraintOwner
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
            ISetCalculationConstraintsCommand command,
            IFactoryProvider<IConstraintFactory> constraintFactoryProvider,
            IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider,
            IMapper<ArgumentDTO, Argument> argumentDTOMapper)
        {
            constraints.Clear();

            command.Constraints.ForEach(
                constraint =>
                AddConstraint(
                    constraintFactoryProvider.Get(constraint.ConstraintTypeDTO.Name).Create(constraint.ArgumentDto.Id,
                                                                                            argumentAnswerFactoryProvider.
                                                                                                Get(
                                                                                                constraint.ArgumentDto.
                                                                                                    AnswerType).Create(
                                                                                                constraint.ArgumentDto.
                                                                                                    Name,
                                                                                                constraint.Value))));

            return this;
        }

        public void ProcessCommand(IChangeCalculationCommand command)
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
