using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class ConstraintRepository : IConstraintRepository
    {
        private readonly IDataTableRepository<IConstraint> repository;
        private readonly IDatabaseGateway gateway;

        public ConstraintRepository(IDataTableRepository<IConstraint> repository, IDatabaseGateway gateway)
        {
            this.repository = repository;
            this.gateway = gateway;
        }

        public void SaveForOwner(IConstraintOwner owner)
        {
            gateway.ExecuteUsing(ConstraintTableAccess.RemoveForOwner(owner));

            var sequence = 1;

            owner.Constraints.ForEach(
                constraint =>
                {
                    gateway.ExecuteUsing(ConstraintTableAccess.Add(owner, constraint, sequence));

                    sequence++;
                });
        }

        public IEnumerable<IConstraint> AllForOwner(Guid ownerId)
        {
            return repository.FetchAllUsing(ConstraintTableAccess.AllForOwner(ownerId));
        }

        public void SetArgumentName(Guid argumentId, string argumentName)
        {
            gateway.ExecuteUsing(ConstraintTableAccess.SetArgumentName(argumentId, argumentName));
        }

        public void SetArgumentAnswerType(Guid argumentId, string answerType)
        {
            gateway.ExecuteUsing(ConstraintTableAccess.SetArgumentAnswerType(argumentId, answerType));
        }
    }
}
