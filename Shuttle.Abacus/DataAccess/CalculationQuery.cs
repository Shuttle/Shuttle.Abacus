using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class CalculationQuery : ICalculationQuery
    {
        private readonly IDataTableMapper<CalculationDTO> _calculationDTOMapper;
        private readonly ICalculationQueryFactory _calculationQueryFactory;
        private readonly IDatabaseGateway _databaseGateway;

        public CalculationQuery(IDatabaseGateway databaseGateway, ICalculationQueryFactory calculationQueryFactory,
            IDataTableMapper<CalculationDTO> calculationDTOMapper)
        {
            _databaseGateway = databaseGateway;
            _calculationQueryFactory = calculationQueryFactory;
            _calculationDTOMapper = calculationDTOMapper;
        }

        public IEnumerable<DataRow> AllForOwner(Guid ownerId)
        {
            return _databaseGateway.GetRowsUsing(_calculationQueryFactory.AllForOwner(ownerId));
        }

        public DataTable AllBeforeCalculation(Guid methodId, Guid calculationId)
        {
            return
                _databaseGateway.GetDataTableFor(_calculationQueryFactory.AllBeforeCalculation(methodId, calculationId));
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_calculationQueryFactory.Get(id));
        }

        public IEnumerable<DataRow> AllForMethod(Guid methodId)
        {
            return _databaseGateway.GetRowsUsing(_calculationQueryFactory.AllForMethod(methodId));
        }

        public IEnumerable<CalculationDTO> DTOsBeforeCalculation(Guid methodId, Guid calculationId)
        {
            return _calculationDTOMapper.MapFrom(AllBeforeCalculation(methodId, calculationId));
        }

        public IEnumerable<CalculationDTO> DTOsForMethod(Guid methodId)
        {
            return _calculationDTOMapper.MapFrom(AllForMethod(methodId).CopyToDataTable());
        }

        public IEnumerable<DataRow> AllForMethod(Guid methodId, Guid grabberCalculationId)
        {
            return _databaseGateway.GetRowsUsing(_calculationQueryFactory.AllForMethod(methodId, grabberCalculationId));
        }

        public IEnumerable<DataRow> GraphNodeArguments(Guid calculationId)
        {
            return _databaseGateway.GetRowsUsing(_calculationQueryFactory.GraphNodeArguments(calculationId));
        }
    }
}