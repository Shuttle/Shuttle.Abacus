using System.Data;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodDataRowMapper : IDataRowMapper<Method>
    {
        private readonly ICalculationRepository calculationRepository;
        private readonly ILimitRepository limitRepository;

        public MethodDataRowMapper(ICalculationRepository calculationRepository, ILimitRepository limitRepository)
        {
            this.calculationRepository = calculationRepository;
            this.limitRepository = limitRepository;
        }

        public Method MapFrom(DataRow input)
        {
            var method = new Method(MethodColumns.Id.MapFrom(input))
                          {
                              MethodName = MethodColumns.Name.MapFrom(input)
                          };

            if (UnitOfWork.Uses<Calculation>())
            {
                calculationRepository.AllForOwner(method.Id).ForEach(calculation => method.AddCalculation(calculation));
            }

            limitRepository.AllForOwner(method.Id).ForEach(limit => method.AddLimit(limit));

            return method;
        }
    }
}
