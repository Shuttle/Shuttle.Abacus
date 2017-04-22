using System.Data;
using Shuttle.Abacus.Domain;

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

        public Calculation MapFrom(DataRow input)
        {
            var id = CalculationColumns.Id.MapFrom(input);

            if (UnitOfWork.Contains(id))
            {
                return UnitOfWork.Get<Calculation>(id);
            }

            var calculation =
                factoryProvider.Get(CalculationColumns.Type.MapFrom(input)).Create(
                    CalculationColumns.Name.MapFrom(input),
                    CalculationColumns.Required.MapFrom(input));

            calculation.AssignId(id);

            var formulaOwner = calculation as IFormulaOwner;

            if (formulaOwner != null)
            {
                formulaRepository.AllForOwner(calculation.Id).ForEach(formula => formulaOwner.AddFormula(formula));
            }

            limitRepository.AllForOwner(calculation.Id).ForEach(limit => calculation.AddLimit(limit));

            if (UnitOfWork.Uses<Calculation>())
            {
                var owner = calculation as ICalculationOwner;

                if (owner != null)
                {
                    DependencyResolver.Resolve<ICalculationRepository>()
                        .AllForOwner(calculation.Id)
                        .ForEach(item => owner.AddCalculation(item));
                }
            }

            graphNodeArgumentDataRowMapper.FetchAllUsing(GraphNodeArgumentQueryFactory.AllForCalculation(calculation)).ForEach(calculation.AddGraphNodeArgument);

            UnitOfWork.Register(calculation);

            return calculation;
        }
    }
}
