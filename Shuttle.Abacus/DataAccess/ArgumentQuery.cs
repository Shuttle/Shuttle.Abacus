using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentQuery : IArgumentQuery
    {
        private readonly IDataTableMapper<ArgumentDTO> _argumentDTOMapper;
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IArgumentQueryFactory _argumentQueryFactory;

        public ArgumentQuery(IDatabaseGateway databaseGateway, IArgumentQueryFactory argumentQueryFactory, IDataTableMapper<ArgumentDTO> argumentDTOMapper)
        {
            _databaseGateway = databaseGateway;
            _argumentQueryFactory = argumentQueryFactory;
            _argumentDTOMapper = argumentDTOMapper;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_argumentQueryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_argumentQueryFactory.Get(id));
        }

        public IEnumerable<DataRow> GetAnswerCatalog(Guid id)
        {
            return _databaseGateway.GetRowsUsing(_argumentQueryFactory.GetRestrictedAnswer(id));
        }

        public ArgumentDTO ArgumentDTO(Guid argumentId)
        {
            return _argumentDTOMapper.MapFrom(Get(argumentId).Table).First();
        }

        public IEnumerable<ArgumentDTO> AllDTOs()
        {
            return _argumentDTOMapper.MapFrom(All().CopyToDataTable());
        }
    }
}