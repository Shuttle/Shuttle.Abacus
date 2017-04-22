using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class DecimalTableQuery : IDecimalTableQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IDataTableMapper<DecimalTableDTO> _decimalTableDTOMapper;
        private readonly IDecimalTableQueryFactory _decimalTableQueryFactory;

        public DecimalTableQuery(IDatabaseGateway databaseGateway, IDecimalTableQueryFactory decimalTableQueryFactory,
            IDataTableMapper<DecimalTableDTO> decimalTableDTOMapper)
        {
            _databaseGateway = databaseGateway;
            _decimalTableQueryFactory = decimalTableQueryFactory;
            _decimalTableDTOMapper = decimalTableDTOMapper;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_decimalTableQueryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_decimalTableQueryFactory.Get(id));
        }

        public DataTable ConstrainedDecimalValues(Guid id)
        {
            return _databaseGateway.GetDataTableFor(_decimalTableQueryFactory.ConstrainedDecimalValues(id));
        }

        public DataTable QueryDecimalTable(Guid decimalTableId)
        {
            return _databaseGateway.GetDataTableFor(_decimalTableQueryFactory.DecimalTableReport(decimalTableId));
        }

        public IEnumerable<DecimalTableDTO> AllDTOs()
        {
            return _decimalTableDTOMapper.MapFrom(_databaseGateway.GetDataTableFor(_decimalTableQueryFactory.All()));
        }
    }
}