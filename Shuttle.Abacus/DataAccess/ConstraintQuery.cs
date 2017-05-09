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

        public IEnumerable<DataRow> AllForOwner(Guid ownerId)
        {
            return _databaseGateway.GetRowsUsing(_constraintQueryFactory.All(ownerId));
        }

        //public void GetOwned(IConstraintOwner owner)
        //{
        //    Guard.AgainstNull(owner, "owner");

        //    foreach (var row in _databaseGateway.GetRowsUsing(_constraintQueryFactory.All(owner.Id)))
        //    {
        //        owner.AddConstraint(new FormulaConstraint(
        //            ConstraintColumns.SequenceNumber.MapFrom(row),
        //            ConstraintColumns.ArgumentId.MapFrom(row),
        //            ConstraintColumns.Name.MapFrom(row),
        //            ConstraintColumns.Answer.MapFrom(row)
        //        ));
        //    }
        //}

        //public void SaveOwned(IConstraintOwner owner)
        //{
        //    Guard.AgainstNull(owner, "owner");

        //    _databaseGateway.ExecuteUsing(_constraintQueryFactory.Remove(owner.Id));

        //    foreach (var constraint in owner.Constraints)
        //    {
        //        _databaseGateway.ExecuteUsing(_constraintQueryFactory.Add(owner, constraint));
        //    }
        //}
    }
}