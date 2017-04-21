using System;
using System.Collections.Generic;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodQuery :IMethodQuery
    {
        private readonly IDataRowMapper<MethodDTO> methodDTOMapper;

        public MethodQuery(IDataRowMapper<MethodDTO> methodDTOMapper)
        {
            this.methodDTOMapper = methodDTOMapper;
        }

        public IQueryResult MethodName(Guid id)
        {
            return QueryProcessor.Execute(MethodQueries.MethodName(id));
        }

        public IQueryResult All()
        {
            return QueryProcessor.Execute(MethodQueries.All());
        }

        public IQueryResult Get(Guid id)
        {
            return QueryProcessor.Execute(MethodQueries.Get(id));
        }

        public IEnumerable<MethodDTO> AllDTOs()
        {
            return methodDTOMapper.MapFrom(All().Table);
        }
    }
}
