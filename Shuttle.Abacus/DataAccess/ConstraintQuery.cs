using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

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
            return _databaseGateway.GetRowsUsing(_constraintQueryFactory.All(ownerId));
        }

        public IEnumerable<ConstraintDTO> DTOsForOwner(Guid ownerId)
        {
            var constraints = new List<ConstraintDTO>();

            foreach (var row in _databaseGateway.GetRowsUsing(_constraintQueryFactory.All(ownerId)))
            {
                var argumentResult = _argumentQuery.Get(ConstraintColumns.ArgumentId.MapFrom(row));

                var constraintName = ConstraintColumns.Name.MapFrom(row);

                //var type =
                //    types.Find(item => item.Name.Equals(constraintName, StringComparison.InvariantCultureIgnoreCase));

                constraints.Add(new ConstraintDTO
                {
                    //ConstraintTypeDTO = new ConstraintTypeDTO
                    //{
                    //    Name = constraintName,
                    //    Text = type != null
                    //        ? type.Text
                    //        : constraintName
                    //},
                    //DataRow = argumentResult,
                    Value = ConstraintColumns.Answer.MapFrom(row)
                });
            }

            return constraints;
        }

        public void GetOwned(IConstraintOwner owner)
        {
            Guard.AgainstNull(owner, "owner");

            foreach (var row in _databaseGateway.GetRowsUsing(_constraintQueryFactory.All(owner.Id)))
            {
                owner.AddConstraint(new OwnedConstraint(
                    ConstraintColumns.SequenceNumber.MapFrom(row),
                    ConstraintColumns.ArgumentId.MapFrom(row),
                    ConstraintColumns.Name.MapFrom(row),
                    ConstraintColumns.Answer.MapFrom(row)
                ));
            }
        }

        public void SaveOwned(IConstraintOwner owner)
        {
            Guard.AgainstNull(owner, "owner");

            _databaseGateway.ExecuteUsing(_constraintQueryFactory.Remove(owner.Id));

            foreach (var constraint in owner.Constraints)
            {
                _databaseGateway.ExecuteUsing(_constraintQueryFactory.Add(owner, constraint));
            }
        }
    }
}