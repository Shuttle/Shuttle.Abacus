using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class ConstraintRepository : IConstraintRepository
    {
        private readonly IDataTableRepository<IConstraint> _repository;
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IConstraintQueryFactory _constraintQueryFactory;

        public ConstraintRepository(IDatabaseGateway databaseGateway, IConstraintQueryFactory constraintQueryFactory, IDataTableRepository<IConstraint> repository)
        {
            this._repository = repository;
            this._databaseGateway = databaseGateway;
            _constraintQueryFactory = constraintQueryFactory;
        }

        public void SaveForOwner(IConstraintOwner owner)
        {
            _databaseGateway.ExecuteUsing(_constraintQueryFactory.RemoveForOwner(owner));

            var sequence = 1;

            owner.Constraints.ForEach(
                constraint =>
                {
                    _databaseGateway.ExecuteUsing(_constraintQueryFactory.Add(owner, constraint, sequence));

                    sequence++;
                });
        }

        public IEnumerable<IConstraint> AllForOwner(Guid ownerId)
        {
            return _repository.FetchAllUsing(_constraintQueryFactory.AllForOwner(ownerId));
        }

        public void SetArgumentName(Guid argumentId, string argumentName)
        {
            _databaseGateway.ExecuteUsing(_constraintQueryFactory.SetArgumentName(argumentId, argumentName));
        }

        public void SetArgumentAnswerType(Guid argumentId, string answerType)
        {
            _databaseGateway.ExecuteUsing(_constraintQueryFactory.SetArgumentAnswerType(argumentId, answerType));
        }
    }
}
