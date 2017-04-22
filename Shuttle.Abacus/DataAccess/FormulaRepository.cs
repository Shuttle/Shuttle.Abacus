using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaRepository : Repository<Formula>, IFormulaRepository
    {
        private readonly IConstraintRepository _constraintRepository;
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IFormulaQueryFactory _formulaQueryFactory;
        private readonly IDataRepository<Formula> _repository;

        public FormulaRepository(IDatabaseGateway databaseGateway, IFormulaQueryFactory formulaQueryFactory, IDataRepository<Formula> repository, IConstraintRepository constraintRepository)
        {
            _repository = repository;
            _constraintRepository = constraintRepository;
            _databaseGateway = databaseGateway;
            _formulaQueryFactory = formulaQueryFactory;
        }

        public override void Add(Formula item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Formula item)
        {
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.Remove(item));
        }

        public override Formula Get(Guid id)
        {
            var result = _repository.FetchItemUsing(_formulaQueryFactory.Get(id));

            if (result == null)
            {
                throw Exceptions.MissingEntity<Formula>(id);
            }

            return result;
        }

        public void Add(IFormulaOwner owner, Formula formula)
        {
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.Add(owner, formula));

            AddOperations(formula);

            _constraintRepository.SaveForOwner(formula);
        }

        public void Save(Formula item)
        {
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.Save(item));
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.RemoveOperations(item));

            AddOperations(item);

            _constraintRepository.SaveForOwner(item);
        }

        public void SaveOrdered(IFormulaOwner owner)
        {
            var sequence = 1;

            owner.Formulas.ForEach(formula =>
            {
                _databaseGateway.ExecuteUsing(_formulaQueryFactory.SetSequenceNumber(formula, sequence));

                sequence++;
            });
        }

        public IEnumerable<Formula> AllForOwner(Guid ownerId)
        {
            return _repository.FetchAllUsing(_formulaQueryFactory.AllForOwner(ownerId));
        }

        private void AddOperations(Formula formula)
        {
            var sequence = 1;

            formula.Operations.ForEach(operation =>
            {
                _databaseGateway.ExecuteUsing(_formulaQueryFactory.AddOperation(formula, operation, sequence));

                sequence++;
            });
        }
    }
}