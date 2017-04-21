using System;
using System.Collections.Generic;
using Abacus.DTO;
namespace Abacus.Data
{
    public class MethodQuery : DataQuery, IMethodQuery
    {
        private readonly IDataTableMapper<MethodDTO> methodDTOMapper;

        public MethodQuery(IDataTableMapper<MethodDTO> methodDTOMapper)
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
