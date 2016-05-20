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

namespace Shuttle.Abacus
{
    public class EqualsConstraint : Constraint
    {
        public EqualsConstraint(Guid argumentId, ArgumentAnswer argumentAnswer)
            : base(argumentId, argumentAnswer)
        {
        }

        public override string Name
        {
            get { return "Equals"; }
        }

        public override string Text
        {
            get { return "Equals"; }
        }

        public override bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            var answer = collectionMethodContext.GetArgumentAnswer(ArgumentAnswer.ArgumentName);

            return !answer.IsNull ? answer.CompareTo(ArgumentAnswer) == 0 : false;
        }

        public override string Description()
        {
            return string.Format("'{0}' equals '{1}'", ArgumentAnswer.ArgumentName, ArgumentAnswer.Description());
        }

        public override IConstraint Copy()
        {
            return new EqualsConstraint(ArgumentId, ArgumentAnswer);
        }
    }
}
