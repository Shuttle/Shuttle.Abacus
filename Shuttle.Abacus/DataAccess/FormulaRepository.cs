using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaRepository : Repository<Formula>, IFormulaRepository
    {
        private readonly IConstraintRepository constraintRepository;
        private readonly IDataRepository<Formula> repository;
        private readonly IDatabaseGateway gateway;

        public FormulaRepository(IDataRepository<Formula> repository, IDatabaseGateway gateway, IConstraintRepository constraintRepository)
        {
            this.repository = repository;
            this.constraintRepository = constraintRepository;
            this.gateway = gateway;
        }

        public override void Add(Formula item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Formula item)
        {
            gateway.ExecuteUsing(FormulaTableAccess.Remove(item));
        }

        public override Formula Get(Guid id)
        {
            var result = repository.FetchItemUsing(FormulaTableAccess.Get(id));

            Guard.AgainstMissing<Formula>(result, id);

            return result;
        }

        public void Add(IFormulaOwner owner, Formula formula)
        {
            gateway.ExecuteUsing(FormulaTableAccess.Add(owner, formula));

            AddOperations(formula);

            constraintRepository.SaveForOwner(formula);
        }

        private void AddOperations(Formula formula)
        {
            var sequence = 1;

            formula.Operations.ForEach(operation =>
                {
                    gateway.ExecuteUsing(FormulaTableAccess.AddOperation(formula, operation, sequence));

                    sequence++;
                });
        }

        public void Save(Formula item)
        {
            gateway.ExecuteUsing(FormulaTableAccess.Save(item));
            gateway.ExecuteUsing(FormulaTableAccess.RemoveOperations(item));

            AddOperations(item);

            constraintRepository.SaveForOwner(item);
        }

        public void SaveOrdered(IFormulaOwner owner)
        {
            var sequence = 1;

            owner.Formulas.ForEach(formula =>
                {
                    gateway.ExecuteUsing(FormulaTableAccess.SetSequenceNumber(formula, sequence));

                    sequence++;
                });
        }

        public IEnumerable<Formula> AllForOwner(Guid ownerId)
        {
            return repository.FetchAllUsing(FormulaTableAccess.AllForOwner(ownerId));
        }
    }
}
