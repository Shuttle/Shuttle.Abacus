using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodQuery : IMethodQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IDataTableMapper<MethodDTO> _methodDTOMapper;
        private readonly IMethodQueryFactory _methodQueryFactory;

        public MethodQuery(IDatabaseGateway databaseGateway, IMethodQueryFactory methodQueryFactory,
            IDataTableMapper<MethodDTO> methodDTOMapper)
        {
            _databaseGateway = databaseGateway;
            _methodQueryFactory = methodQueryFactory;
            _methodDTOMapper = methodDTOMapper;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_methodQueryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_methodQueryFactory.Get(id));
        }

        public IEnumerable<MethodDTO> AllDTOs()
        {
            return _methodDTOMapper.MapFrom(All().CopyToDataTable());
        }
    }
}