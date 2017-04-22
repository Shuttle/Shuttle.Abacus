using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class LimitDataRowMapper : IDataRowMapper<Limit>
    {
        private readonly IFactoryProvider<ILimitFactory> limitFactoryProvider;
        private readonly IFormulaQuery formulaQuery;
        private readonly IFormulaRepository formulaRepository;

        public LimitDataRowMapper(IFactoryProvider<ILimitFactory> limitFactoryProvider, IFormulaQuery formulaQuery, IFormulaRepository formulaRepository)
        {
            this.limitFactoryProvider = limitFactoryProvider;
            this.formulaQuery = formulaQuery;
            this.formulaRepository = formulaRepository;
        }

        public MappedRow<Limit> Map(DataRow row)
        {
            var result = limitFactoryProvider.Get(LimitColumns.Type.MapFrom(row)).Create(LimitColumns.Name.MapFrom(row));

            result.AssignId(LimitColumns.Id.MapFrom(row));

            foreach (var formulaRow in formulaQuery.AllForOwner(result.Id))
            {
                result.AddFormula(formulaRepository.Get(FormulaColumns.Id.MapFrom(formulaRow)));
            }

            return new MappedRow<Limit>(row, result);
        }
    }
}
