using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class DecimalTable :
        ISpecification<IMethodContext>
    {
        private static readonly DecimalValue ZeroDecimalValue = new DecimalValue(0);

        private readonly List<DecimalValue> values = new List<DecimalValue>();

        public DecimalTable(Guid id, string name, Guid rowArgumentId, Guid? columnArgumentId)
        {
            Id = id;
            Name = name;
            RowArgumentId = rowArgumentId;

            ColumnArgumentId = Guid.Empty.Equals(columnArgumentId)
                                 ? null
                                 : columnArgumentId;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public IEnumerable<DecimalValue> DecimalValues
        {
            get { return new ReadOnlyCollection<DecimalValue>(values); }
        }

        public Guid RowArgumentId { get; private set; }
        public Guid? ColumnArgumentId { get; private set; }

        public bool IsSatisfiedBy(IMethodContext item)
        {
            foreach (var value in DecimalValues)
            {
                if (value.IsSatisfiedBy(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void AddDecimalValue(DecimalValue decimalValue)
        {
            Guard.AgainstNull(decimalValue, "decimalValue");

            values.Add(decimalValue);
        }

        public DecimalValue Get(IMethodContext collectionContext)
        {
            var result = Find(collectionContext);

            if (result != null)
            {
                return result;
            }

            collectionContext.AddErrorMessage(string.Format("Could not find a qualifying value in decimal table '{0}'.", Name));

            return ZeroDecimalValue;
        }

        public DecimalValue Find(IMethodContext collectionContext)
        {
            return values.Find(value => value.IsSatisfiedBy(collectionContext));
        }

        public DecimalTable Copy()
        {
            var result = new DecimalTable(Guid.NewGuid(), Name, RowArgumentId, ColumnArgumentId);

            values.ForEach(value => result.AddDecimalValue(value.Copy()));

            return result;
        }
    }
}
