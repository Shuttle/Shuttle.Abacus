using System.Collections.Generic;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class AnswerTypeQuery : IAnswerTypeQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IAnswerTypeQueryFactory _answerTypeQueryFactory;
        private readonly IQueryMapper _queryMapper;

        public AnswerTypeQuery(IDatabaseGateway databaseGateway, IAnswerTypeQueryFactory answerTypeQueryFactory, IQueryMapper queryMapper)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(answerTypeQueryFactory, "answerTypeQueryFactory");
            Guard.AgainstNull(queryMapper, "queryMapper");

            _databaseGateway = databaseGateway;
            _answerTypeQueryFactory = answerTypeQueryFactory;
            _queryMapper = queryMapper;
        }

        public IEnumerable<AnswerTypeDTO> All()
        {
            return _queryMapper.MapObjects<AnswerTypeDTO>(_answerTypeQueryFactory.All());
        }
    }
}
