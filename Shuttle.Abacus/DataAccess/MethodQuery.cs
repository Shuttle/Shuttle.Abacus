using System;
using System.Collections.Generic;

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
            return QueryProcessor.Execute(MethodQueryFactory.MethodName(id));
        }

        public IQueryResult All()
        {
            return QueryProcessor.Execute(MethodQueryFactory.All());
        }

        public IQueryResult Get(Guid id)
        {
            return QueryProcessor.Execute(MethodQueryFactory.Get(id));
        }

        public IEnumerable<MethodDTO> AllDTOs()
        {
            return methodDTOMapper.MapFrom(All().Table);
        }
    }
}
