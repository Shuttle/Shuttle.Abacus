using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class ConstraintQuery : IConstraintQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IConstraintQueryFactory _constraintQueryFactory;
        private readonly IArgumentQuery _argumentQuery;

        public ConstraintQuery(IDatabaseGateway databaseGateway, IConstraintQueryFactory constraintQueryFactory, IArgumentQuery argumentQuery)
        {
            _databaseGateway = databaseGateway;
            _constraintQueryFactory = constraintQueryFactory;
            _argumentQuery = argumentQuery;
        }

        public IEnumerable<DataRow> QueryAllForOwner(Guid ownerId)
        {
            return _databaseGateway.GetRowsUsing(_constraintQueryFactory.AllForOwner(ownerId));
        }

        //public IEnumerable<ConstraintDTO> DTOsForOwner(Guid ownerId)
        //{
        //    var constraints = new List<ConstraintDTO>();

        //    var types = new List<ConstraintTypeDTO>(ConstraintTypes());

        //    foreach (
        //        DataRow row in
        //        QueryProcessor.Execute(ConstraintQueryFactory.DTOsForOwner(ownerId)).Table.Rows)
        //    {
        //        var argumentResult = _argumentQuery.ArgumentDTO(ConstraintColumns.ArgumentId.MapFrom(row));

        //        var constraintName = ConstraintColumns.Name.MapFrom(row);

        //        var type =
        //            types.Find(item => item.Name.Equals(constraintName, StringComparison.InvariantCultureIgnoreCase));

        //        constraints.Add(new ConstraintDTO
        //        {
        //            ConstraintTypeDTO = new ConstraintTypeDTO
        //            {
        //                Name = constraintName,
        //                Text = type != null
        //                    ? type.Text
        //                    : constraintName
        //            },
        //            ArgumentDTO = argumentResult,
        //            Value = ConstraintColumns.Answer.MapFrom(row)
        //        });
        //    }

        //    return constraints;
        //}
    }
}