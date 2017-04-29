using System.Data;
using System.Runtime.Remoting.Messaging;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaDataRowMapper : IDataRowMapper<Formula>
    {
        private readonly IConstraintRepository constraintRepository;
        private readonly IFactoryProvider<IOperationFactory> operationFactoryProvider;
        private readonly IFormulaQuery query;
        private readonly IFactoryProvider<IValueSourceFactory> valueSourceFactoryProvider;

        public FormulaDataRowMapper(IFormulaQuery query, IConstraintRepository constraintRepository,
                                    IFactoryProvider<IOperationFactory> operationFactoryProvider,
                                    IFactoryProvider<IValueSourceFactory> valueSourceFactoryProvider)
        {
            this.query = query;
            this.constraintRepository = constraintRepository;
            this.operationFactoryProvider = operationFactoryProvider;
            this.valueSourceFactoryProvider = valueSourceFactoryProvider;
        }

        public MappedRow<Formula> Map(DataRow row)
        {
            var id = FormulaColumns.Id.MapFrom(row);

            var formula = new Formula(id);

            foreach (DataRow operationRow in query.Operations(formula.Id))
            {
                formula.AddOperation(
                    operationFactoryProvider.Get(FormulaOperationColumns.Operation.MapFrom(operationRow)).Create(
                        valueSourceFactoryProvider.Get(FormulaOperationColumns.ValueSource.MapFrom(operationRow)).Create(
                            FormulaOperationColumns.ValueSelection.MapFrom(operationRow))));
            }

            foreach (var constraint in constraintRepository.AllForOwner(formula.Id))
            {
                formula.AddConstraint(constraint);
            }

            return new MappedRow<Formula>(row, formula);
        }
    }
}
