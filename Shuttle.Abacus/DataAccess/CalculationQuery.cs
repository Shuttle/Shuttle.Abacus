using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class CalculationQuery : ICalculationQuery
    {
        private readonly ICalculationQueryFactory _calculationQueryFactory;
        private readonly IDatabaseGateway _databaseGateway;

        public CalculationQuery(IDatabaseGateway databaseGateway, ICalculationQueryFactory calculationQueryFactory)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(calculationQueryFactory, "calculationQueryFactory");

            _databaseGateway = databaseGateway;
            _calculationQueryFactory = calculationQueryFactory;
        }

        public IEnumerable<DataRow> AllForOwner(Guid ownerId)
        {
            return _databaseGateway.GetRowsUsing(_calculationQueryFactory.AllForOwner(ownerId));
        }

        public DataTable AllBeforeCalculation(Guid methodId, Guid calculationId)
        {
            return
                _databaseGateway.GetDataTableFor(
                    _calculationQueryFactory.AllBeforeCalculation(methodId, calculationId));
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_calculationQueryFactory.Get(id));
        }

        public IEnumerable<DataRow> AllForMethod(Guid methodId)
        {
            return _databaseGateway.GetRowsUsing(_calculationQueryFactory.AllForMethod(methodId));
        }

        public IEnumerable<DataRow> AllForMethod(Guid methodId, Guid grabberCalculationId)
        {
            return _databaseGateway.GetRowsUsing(_calculationQueryFactory.AllForMethod(methodId, grabberCalculationId));
        }

        public IEnumerable<DataRow> GraphNodeArguments(Guid calculationId)
        {
            return _databaseGateway.GetRowsUsing(_calculationQueryFactory.GraphNodeArguments(calculationId));
        }

        public void PopulateOwner(ICalculationOwner owner)
        {
            Guard.AgainstNull(owner, "owner");

            foreach (var row in _databaseGateway.GetRowsUsing(_calculationQueryFactory.AllForOwner(owner.Id)))
            {
                owner.AddCalculation(
                    new CalculationItem(
                        CalculationColumns.Id.MapFrom(row),
                        CalculationColumns.Name.MapFrom(row),
                        CalculationColumns.Type.MapFrom(row)));
            }
        }
    }
}