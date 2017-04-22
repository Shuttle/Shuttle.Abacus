using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

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

        public MappedRow<Method> Map(DataRow row)
        {
            var method = new Method(MethodColumns.Id.MapFrom(row))
            {
                MethodName = MethodColumns.Name.MapFrom(row)
            };

            //if (UnitOfWork.Uses<Calculation>())
            //{
            //    calculationRepository.AllForOwner(method.Id).ForEach(calculation => method.AddCalculation(calculation));
            //}

            limitRepository.AllForOwner(method.Id).ForEach(limit => method.AddLimit(limit));

            return new MappedRow<Method>(row, method);
        }
    }
}
