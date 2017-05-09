using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixElementRepository : Repository<MatrixElement>, IMatrixElementRepository
    {
        private readonly IDatabaseGateway gateway;
        private readonly IDataRepository<MatrixElement> repository;

        public MatrixElementRepository(IDataRepository<MatrixElement> repository, IDatabaseGateway gateway)
        {
            this.repository = repository;
            this.gateway = gateway;
        }

        public override void Add(MatrixElement item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override MatrixElement Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(Matrix matrix, MatrixElement matrixElement)
        {
            gateway.ExecuteUsing(MatrixElementQueryFactory.Add(matrix, matrixElement));
        }

        public IEnumerable<MatrixElement> AllForDecimalTable(Matrix matrix)
        {
            return repository.FetchAllUsing(MatrixElementQueryFactory.AllForDecimalTable(matrix.Id));
        }

        public void RemoveAllForDecimalTable(Guid decimalTableId)
        {
            gateway.ExecuteUsing(MatrixElementQueryFactory.RemoveConstraintsForDecimalTable(decimalTableId));
            gateway.ExecuteUsing(MatrixElementQueryFactory.RemoveAllForDecimalTable(decimalTableId));
        }
    }
}