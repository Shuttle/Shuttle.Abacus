using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.Events.Matrix.v1;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class Matrix
    {
        //private static readonly MatrixElement ZeroMatrixElement = new MatrixElement(0);

        private readonly List<MatrixElement> _values = new List<MatrixElement>();

        public Matrix(Guid id)
        {
            Id = id;
        }

        public Registered Register(string name, string rowArgumentName, string columnArgumentName)
        {
            Guard.AgainstNullOrEmptyString(name, "name");
            Guard.AgainstNullOrEmptyString(rowArgumentName, "rowArgumentName");

            return On(new Registered
            {
                Name = name,
                RowArgumentName = rowArgumentName,
                ColumnArgumentName = columnArgumentName ?? string.Empty
            });
        }

        private Registered On(Registered registered)
        {
            throw new NotImplementedException();
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public IEnumerable<MatrixElement> Elements => new ReadOnlyCollection<MatrixElement>(_values);

        public string RowArgumentName { get; private set; }
        public string ColumnArgumentName { get; private set; }

        public static string Key(string name)
        {
            return string.Format("[matrix]:name={0}", name);
        }

        public void AddElement(MatrixElement matrixElement)
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

        public Matrix Copy()
        {
            throw new NotImplementedException();
            //var result = new Matrix(Guid.NewGuid(), Name, RowArgumentId, ColumnArgumentId);

            //_values.ForEach(value => result.AddElement(value.Copy()));

            //return result;
        }
    }
}
