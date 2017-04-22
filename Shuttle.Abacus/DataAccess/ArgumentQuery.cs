using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentQuery : IArgumentQuery
    {
        private readonly IDataTableMapper<ArgumentDTO> _argumentDTOMapper;
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IArgumentQueryFactory _argumentQueryFactory;
        private readonly IPipeline _pipeline;

        public ArgumentQuery(IDatabaseGateway databaseGateway, IArgumentQueryFactory argumentQueryFactory, IDataTableMapper<ArgumentDTO> argumentDTOMapper,
            IPipeline pipeline)
        {
            _databaseGateway = databaseGateway;
            _argumentQueryFactory = argumentQueryFactory;
            _argumentDTOMapper = argumentDTOMapper;
            _pipeline = pipeline;
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
            throw new NotImplementedException();
            //return _argumentDTOMapper.MapFrom(Get(argumentId).Table);
        }
    }
}