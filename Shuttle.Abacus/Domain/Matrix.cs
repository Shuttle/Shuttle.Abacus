using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class Matrix :
        ISpecification<IMethodContext>
    {
        //private static readonly MatrixElement ZeroMatrixElement = new MatrixElement(0);

        private readonly List<MatrixElement> _values = new List<MatrixElement>();

        public Matrix(Guid id, string name, Guid rowArgumentId, Guid? columnArgumentId)
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

        public IEnumerable<MatrixElement> Elements
        {
            get { return new ReadOnlyCollection<MatrixElement>(_values); }
        }

        public Guid RowArgumentId { get; private set; }
        public Guid? ColumnArgumentId { get; private set; }

        public bool IsSatisfiedBy(IMethodContext item)
        {
            throw new NotImplementedException();
            //foreach (var value in Elements)
            //{
            //    if (value.IsSatisfiedBy(item))
            //    {
            //        return true;
            //    }
            //}

            //return false;
        }

        public void AddDecimalValue(MatrixElement matrixElement)
        {
            Guard.AgainstNull(matrixElement, "matrixElement");

            _values.Add(matrixElement);
        }

        //public MatrixElement Get(IMethodContext collectionContext)
        //{
        //    var result = Find(collectionContext);

        //    if (result != null)
        //    {
        //        return result;
        //    }

        //    collectionContext.AddErrorMessage(string.Format("Could not find a qualifying value in decimal table '{0}'.", Name));

        //    return ZeroMatrixElement;
        //}

        public MatrixElement Find(IMethodContext collectionContext)
        {
            throw new NotImplementedException();
            //return _values.Find(value => value.IsSatisfiedBy(collectionContext));
        }

        public Matrix Copy()
        {
            throw new NotImplementedException();
            //var result = new Matrix(Guid.NewGuid(), Name, RowArgumentId, ColumnArgumentId);

            //_values.ForEach(value => result.AddDecimalValue(value.Copy()));

            //return result;
        }
    }
}
