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
