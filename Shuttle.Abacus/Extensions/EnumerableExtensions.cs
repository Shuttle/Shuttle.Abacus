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

namespace Shuttle.Abacus
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<ConstraintTypeDTO> ConstraintTypes(this IEnumerable<IConstraintFactory> factories)
        {
            var result = new List<ConstraintTypeDTO>();

            foreach (var factory in factories)
            {
                result.Add(new ConstraintTypeDTO
                {
                    Name = factory.Name,
                    Text = factory.Text
                });
            }

            result.Sort((x, y) => String.Compare(x.Text, y.Text, StringComparison.Ordinal));

            return result;
        }

        public static IEnumerable<OperationTypeDTO> OperationTypes(this IEnumerable<IOperationFactory> factories)
        {
            var result = new List<OperationTypeDTO>();

            foreach (var factory in factories)
            {
                result.Add(new OperationTypeDTO
                {
                    Name = factory.Name,
                    Text = factory.Text
                });
            }

            result.Sort((x, y) => x.Text.CompareTo(y.Text));

            return result;
        }

        public static IEnumerable<AnswerTypeDTO> ValueTypes(this IEnumerable<IArgumentAnswerFactory> factories)
        {
            var result = new List<AnswerTypeDTO>();

            foreach (var factory in factories)
            {
                result.Add(new AnswerTypeDTO
                {
                    Name = factory.Name,
                    Text = factory.Text
                });
            }

            result.Sort((x, y) => x.Text.CompareTo(y.Text));

            return result;
        }
    }
}
