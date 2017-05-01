using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class LimitRepository : Repository<Limit>, ILimitRepository
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly ILimitQueryFactory _limitQueryFactory;
        private readonly IFormulaQueryFactory _formulaQueryFactory;
        private readonly IConstraintQueryFactory _constraintQueryFactory;

        public LimitRepository(IDatabaseGateway databaseGateway, ILimitQueryFactory limitQueryFactory, 
            IFormulaQueryFactory formulaQueryFactory, IConstraintQueryFactory constraintQueryFactory)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(limitQueryFactory, "limitQueryFactory");
            Guard.AgainstNull(formulaQueryFactory, "formulaQueryFactory");
            Guard.AgainstNull(constraintQueryFactory, "constraintQueryFactory");

            _databaseGateway = databaseGateway;
            _limitQueryFactory = limitQueryFactory;
            _formulaQueryFactory = formulaQueryFactory;
            _constraintQueryFactory = constraintQueryFactory;
        }

        public void Add(string ownerName, Guid ownerId, Limit limit)
        {
            _databaseGateway.ExecuteUsing(_limitQueryFactory.Add(ownerName, ownerId, limit));
        }

        public override void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_limitQueryFactory.Remove(id));
        }

        public override Limit Get(Guid id)
        {
            var limitRow = _databaseGateway.GetSingleRowUsing(_limitQueryFactory.Get(id));

            if (limitRow == null)
            {
                throw Exceptions.MissingEntity("Limit", id);
            }

            var result = new Limit(id, LimitColumns.Name.MapFrom(limitRow), LimitColumns.Type.MapFrom(limitRow));

            foreach (var row in _databaseGateway.GetRowsUsing(_formulaQueryFactory.AllForOwner(id)))
            {
                result.AddFormula(
                    new OwnedFormula(
                        FormulaColumns.Id.MapFrom(row),
                        FormulaColumns.SequenceNumber.MapFrom(row),
                        FormulaColumns.Description.MapFrom(row)));
            }

            foreach (var row in _databaseGateway.GetRowsUsing(_constraintQueryFactory.All(id)))
            {
                result.AddConstraint(
                    new OwnedConstraint(
                    ConstraintColumns.SequenceNumber.MapFrom(row),
                    ConstraintColumns.ArgumentId.MapFrom(row),
                    ConstraintColumns.Name.MapFrom(row),
                    ConstraintColumns.Answer.MapFrom(row)));
            }

            return result;
        }

        public void Save(Limit limit)
        {
            _databaseGateway.ExecuteUsing(_limitQueryFactory.Save(limit));
        }

        public override void Add(Limit item)
        {
            throw new NotImplementedException();
        }
    }
}