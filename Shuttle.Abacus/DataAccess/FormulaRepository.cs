using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaRepository : Repository<Formula>, IFormulaRepository
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IFormulaQueryFactory _formulaQueryFactory;
        private readonly IConstraintQuery _constraintQuery;

        public FormulaRepository(IDatabaseGateway databaseGateway, IFormulaQueryFactory formulaQueryFactory, IConstraintQuery constraintQuery)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(formulaQueryFactory, "formulaQueryFactory");
            Guard.AgainstNull(constraintQuery, "constraintQuery");

            _databaseGateway = databaseGateway;
            _formulaQueryFactory = formulaQueryFactory;
            _constraintQuery = constraintQuery;
        }

        public override void Add(Formula formula)
        {
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.Add(formula));

            AddOperations(formula);

            _constraintQuery.SaveOwned(formula);
        }

        public override void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.Remove(id));
        }

        public override Formula Get(Guid id)
        {
            var formulaRow = _databaseGateway.GetSingleRowUsing(_formulaQueryFactory.Get(id));

            Guarded.Entity<Formula>(formulaRow, id);

            var result = new Formula(id, FormulaColumns.Name.MapFrom(formulaRow));

            foreach (var row in _databaseGateway.GetRowsUsing(_formulaQueryFactory.GetOperations(id)))
            {
                result.AddOperation(new FormulaOperation(
                    FormulaOperationColumns.SequenceNumber.MapFrom(row),
                    FormulaOperationColumns.Operation.MapFrom(row),
                    FormulaOperationColumns.ValueSource.MapFrom(row),
                    FormulaOperationColumns.ValueSelection.MapFrom(row),
                    FormulaOperationColumns.Text.MapFrom(row)));
            }

            _constraintQuery.GetOwned(result);

            return result;
        }

        public void Save(Formula item)
        {
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.Save(item));
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.RemoveOperations(item));

            AddOperations(item);

            _constraintQuery.SaveOwned(item);
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