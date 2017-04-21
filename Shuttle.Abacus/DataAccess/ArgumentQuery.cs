using System;
using System.Collections.Generic;
using Abacus.DTO;
using Abacus.Infrastructure;

namespace Abacus.Data
{
    public class ArgumentQuery : DataQuery, IArgumentQuery
    {
        private readonly IDataTableMapper<ArgumentDTO> argumentDTOMapper;
        private readonly IPipeline pipeline;

        public ArgumentQuery(IDataTableMapper<ArgumentDTO> argumentDTOMapper, IPipeline pipeline)
        {
            this.argumentDTOMapper = argumentDTOMapper;
            this.pipeline = pipeline;
        }

        public IQueryResult All()
        {
            return QueryProcessor.Execute(ArgumentQueries.All());
        }

        public IQueryResult Get(Guid id)
        {
            return QueryProcessor.Execute(ArgumentQueries.Get(id));
        }

        public IQueryResult GetAnswerCatalog(Guid id)
        {
            return QueryProcessor.Execute(ArgumentQueries.GetRestrictedAnswer(id));
        }

        public IQueryResult Definitions()
        {
            return QueryProcessor.Execute(ArgumentQueries.Definitions());
        }

        public IQueryResult Name(Guid id)
        {
            return QueryProcessor.Execute(ArgumentQueries.Name(id));
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
            var dtos = new List<ArgumentDTO>(argumentDTOMapper.MapFrom(QueryProcessor.Execute(ArgumentQueries.Definition(argumentId)).Table));

            var dto = dtos.Count > 0 ? dtos[0]: null;

            Guard.AgainstMissing<ArgumentDTO>(dto, argumentId);

            return dto;
        }
    }
}
