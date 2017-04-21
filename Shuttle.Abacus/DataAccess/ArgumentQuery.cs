using System;
using System.Collections.Generic;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentQuery : IArgumentQuery
    {
        private readonly IDataRowMapper<ArgumentDTO> argumentDTOMapper;
        private readonly IPipeline pipeline;

        public ArgumentQuery(IDataRowMapper<ArgumentDTO> argumentDTOMapper, IPipeline pipeline)
        {
            this.argumentDTOMapper = argumentDTOMapper;
            this.pipeline = pipeline;
        }

        public IQueryResult All()
        {
            return QueryProcessor.Execute(ArgumentQueryFactory.All());
        }

        public IQueryResult Get(Guid id)
        {
            return QueryProcessor.Execute(ArgumentQueryFactory.Get(id));
        }

        public IQueryResult GetAnswerCatalog(Guid id)
        {
            return QueryProcessor.Execute(ArgumentQueryFactory.GetRestrictedAnswer(id));
        }

        public IQueryResult Definitions()
        {
            return QueryProcessor.Execute(ArgumentQueryFactory.Definitions());
        }

        public IQueryResult Name(Guid id)
        {
            return QueryProcessor.Execute(ArgumentQueryFactory.Name(id));
        }

        public IEnumerable<ArgumentDTO> AllDTOs()
        {
            var dtos = new List<ArgumentDTO>(argumentDTOMapper.MapFrom(Definitions().Table));

            new List<ArgumentDTO>(argumentDTOMapper.MapFrom(Definitions().Table))
                .ForEach(dto =>
                    {
                        if (
                            dtos.Find(
                                candidate =>
                                candidate.Name.Equals(dto.Name, StringComparison.InvariantCultureIgnoreCase)) ==
                            null)
                        {
                            dtos.Add(dto);
                        }
                    });

            dtos.Sort((x, y) => x.Name.CompareTo(y.Name));

            dtos.ForEach(pipeline.Process);

            return dtos;
        }

        public ArgumentDTO ArgumentDTO(Guid argumentId)
        {
            var dtos = new List<ArgumentDTO>(argumentDTOMapper.MapFrom(QueryProcessor.Execute(ArgumentQueryFactory.Definition(argumentId)).Table));

            var dto = dtos.Count > 0 ? dtos[0]: null;

            Guard.AgainstMissing<ArgumentDTO>(dto, argumentId);

            return dto;
        }
    }
}
