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

using System.Collections.Generic;
using System.Collections.ObjectModel;

// TODO: fix up this class

namespace Shuttle.Abacus
{
    public class FactoryProvider<T> : IFactoryProvider<T> where T : IFactory
    {
        private readonly Dictionary<string, T> factories = new Dictionary<string, T>();

        public FactoryProvider()
        {
            //DependencyResolver.Resolver.ResolveAssignable<T>().ForEach(factory => factories.Add(factory.Name.ToLower(), factory));
        }

        public T Get(string name)
        {
            if (!factories.ContainsKey(name.ToLower()))
            {
                //throw new KeyNotFoundException(string.Format(Resources.KeyNotFoundException, name, typeof(FactoryProvider<T>).Name));
            }

            return factories[name.ToLower()];
        }

        public IEnumerable<T> Factories
        {
            get
            {
                var result = new List<T>();

                //factories.ForEach(entry => result.Add(entry.Value));
                
                return new ReadOnlyCollection<T>(result);
            }
        }
    }
}
