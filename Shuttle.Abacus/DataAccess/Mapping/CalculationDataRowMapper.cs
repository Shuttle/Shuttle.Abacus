using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class CalculationDataRowMapper : IDataRowMapper<Calculation>
    {
        private readonly IFactoryProvider<ICalculationFactory> factoryProvider;
        private readonly IFormulaRepository formulaRepository;
        private readonly ILimitRepository limitRepository;
        private readonly IDataRepository<GraphNodeArgument> graphNodeArgumentDataRowMapper;

        public CalculationDataRowMapper(IFactoryProvider<ICalculationFactory> factoryProvider, IFormulaRepository formulaRepository, ILimitRepository limitRepository, IDataRepository<GraphNodeArgument> graphNodeArgumentDataRowMapper)
        {
            this.factoryProvider = factoryProvider;
            this.formulaRepository = formulaRepository;
            this.limitRepository = limitRepository;
            this.graphNodeArgumentDataRowMapper = graphNodeArgumentDataRowMapper;
        }

        public MappedRow<Calculation> Map(DataRow row)
        {
            var id = CalculationColumns.Id.MapFrom(row);

            var result =
                factoryProvider.Get(CalculationColumns.Type.MapFrom(row)).Create(
                    CalculationColumns.Name.MapFrom(row),
                    CalculationColumns.Required.MapFrom(row));

            result.AssignId(id);

            var formulaOwner = result as IFormulaOwner;

            if (formulaOwner != null)
            {
                formulaRepository.AllForOwner(result.Id).ForEach(formula => formulaOwner.AddFormula(formula));
            }

            limitRepository.AllForOwner(result.Id).ForEach(limit => result.AddLimit(limit));

            //if (UnitOfWork.Uses<Calculation>())
            //{
            //    var owner = calculation as ICalculationOwner;

            //    if (owner != null)
            //    {
            //        DependencyResolver.Resolve<ICalculationRepository>()
            //            .AllForOwner(calculation.Id)
            //            .ForEach(item => owner.AddCalculation(item));
            //    }
            //}

            graphNodeArgumentDataRowMapper.FetchAllUsing(GraphNodeArgumentQueryFactory.AllForCalculation(result)).ForEach(result.AddGraphNodeArgument);

            return new MappedRow<Calculation>(row, result);
        }
    }
}
