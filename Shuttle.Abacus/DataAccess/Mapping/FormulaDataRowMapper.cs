using System.Data;
using Abacus.Domain;
using Abacus.Infrastructure;

namespace Abacus.Data
{
    public class FormulaDataRowMapper : AbstractMapper, IDataRowMapper<Formula>
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

        public Formula MapFrom(DataRow input)
        {
            var id = FormulaColumns.Id.MapFrom(input);

            if (UnitOfWork.Contains(id))
            {
                UnitOfWork.Get<Formula>(id);
            }

            var formula = new Formula(id);

            foreach (DataRow row in query.Operations(formula.Id).Table.Rows)
            {
                formula.AddOperation(
                    operationFactoryProvider.Get(FormulaOperationColumns.Operation.MapFrom(row)).Create(
                        valueSourceFactoryProvider.Get(FormulaOperationColumns.ValueSource.MapFrom(row)).Create(
                            FormulaOperationColumns.ValueSelection.MapFrom(row))));
            }

            constraintRepository.AllForOwner(formula.Id).ForEach(item => formula.AddConstraint(item));

            UnitOfWork.Register(formula);

            return formula;
        }
    }
}
