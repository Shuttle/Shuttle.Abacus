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
using System.Collections;
using System.Collections.Generic;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ArgumentCollection : List<Argument>
    {
        private readonly List<Argument> arguments;

        public ArgumentCollection(IEnumerable<Argument> enumerable)
        {
            arguments = new List<Argument>(enumerable);
        }

        public ArgumentCollection()
        {
            arguments = new List<Argument>();
        }

        public IEnumerator<Argument> GetEnumerator()
        {
            return arguments.GetEnumerator();
        }

        public bool Contains(string name)
        {
            return Find(name) != null;
        }

        public Argument Find(string name)
        {
            return arguments.Find(argument => argument.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Add(Argument argument)
        {
            Guard.AgainstNull(argument, "argument");

            arguments.Add(argument);
        }

        public ArgumentCollection OverrideWith(ArgumentCollection collection)
        {
            var result = new ArgumentCollection();

            collection.ForEach(result.Add);

            arguments.ForEach(argument =>
                {
                    if (!result.Contains(argument.Name))
                    {
                        result.Add(argument);
                    }
                });

            return result;
        }

        public Argument Get(string name)
        {
            var result = Find(name);

            Guard.Against<MissingEntityException>(result == null, name);

            return result;
        }

        public Argument Find(Guid argumentId)
        {
            return arguments.Find(argument => argument.Id.Equals(argumentId));
        }
    }
}
