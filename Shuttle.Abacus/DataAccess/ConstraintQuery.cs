using System;
using System.Collections.Generic;
using System.Data;
using Abacus.DTO;

namespace Abacus.Data
{
    public class ConstraintQuery : DataQuery, IConstraintQuery
    {
        private readonly IDataTableMapper<ConstraintTypeDTO> constraintTypeDTOMapper;
        private readonly IConstraintTypeQuery constraintTypeQuery;
        private readonly IArgumentQuery argumentQuery;

        public ConstraintQuery(IArgumentQuery argumentQuery, IConstraintTypeQuery constraintTypeQuery,
                               IDataTableMapper<ConstraintTypeDTO> constraintTypeDTOMapper)
        {
            this.argumentQuery = argumentQuery;
            this.constraintTypeDTOMapper = constraintTypeDTOMapper;
            this.constraintTypeQuery = constraintTypeQuery;
        }

        public IEnumerable<ConstraintTypeDTO> ConstraintTypes()
        {
            return constraintTypeDTOMapper.MapFrom(constraintTypeQuery.All().Table);
        }

        public IEnumerable<ConstraintDTO> DTOsForOwner(Guid ownerId)
        {
            var constraints = new List<ConstraintDTO>();

            var types = new List<ConstraintTypeDTO>(ConstraintTypes());

            foreach (
                DataRow row in
                    QueryProcessor.Execute(ConstraintQueries.DTOsForOwner(ownerId)).Table.Rows)
            {
                var argumentResult = argumentQuery.ArgumentDTO(ConstraintColumns.ArgumentId.MapFrom(row));

                var constraintName = ConstraintColumns.Name.MapFrom(row);

                var type =
                    types.Find(item => item.Name.Equals(constraintName, StringComparison.InvariantCultureIgnoreCase));

                constraints.Add(new ConstraintDTO
                                {
                                    ConstraintTypeDTO = new ConstraintTypeDTO
                                                        {
                                                            Name = constraintName,
                                                            Text = type != null
                                                                       ? type.Text
                                                                       : constraintName
                                                        },
                                    ArgumentDto = argumentResult,
                                    Value = ConstraintColumns.Answer.MapFrom(row)
                                });
            }

            return constraints;
        }

        public IQueryResult QueryAllForOwner(Guid ownerId)
        {
            return QueryProcessor.Execute(ConstraintQueries.AllForOwner(ownerId));
        }
    }
}
