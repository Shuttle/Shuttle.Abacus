using System.Data;
using Shuttle.Abacus.Domain;

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

        public Limit MapFrom(DataRow input)
        {
            var limit = limitFactoryProvider.Get(LimitColumns.Type.MapFrom(input)).Create(LimitColumns.Name.MapFrom(input));

            limit.AssignId(LimitColumns.Id.MapFrom(input));

            foreach (DataRow row in formulaQuery.AllForOwner(limit.Id).Table.Rows)
            {
                limit.AddFormula(formulaRepository.Get(FormulaColumns.Id.MapFrom(row)));
            }

            return limit;
        }
    }
}
