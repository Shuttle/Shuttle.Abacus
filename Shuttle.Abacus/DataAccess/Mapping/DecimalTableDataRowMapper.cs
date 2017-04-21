using System.Data;
using Abacus.Domain;
using Abacus.Infrastructure;

namespace Abacus.Data
{
    public class DecimalTableDataRowMapper : IDataRowMapper<DecimalTable>
    {
        private readonly IConstraintRepository constraintRepository;
        private readonly IDecimalValueRepository decimalValueRepository;

        public DecimalTableDataRowMapper(IDecimalValueRepository decimalValueRepository,
                                         IConstraintRepository constraintRepository)
        {
            this.decimalValueRepository = decimalValueRepository;
            this.constraintRepository = constraintRepository;
        }

        public DecimalTable MapFrom(DataRow row)
        {
            var result = new DecimalTable(DecimalTableColumns.Id.MapFrom(row),
                                          DecimalTableColumns.Name.MapFrom(row),
                                          DecimalTableColumns.RowArgumentId.MapFrom(row),
                                          DecimalTableColumns.ColumnArgumentId.MapFrom(row));


            decimalValueRepository.AllForDecimalTable(result)
                .ForEach(value =>
                             {
                                 constraintRepository.AllForOwner(value.Id)
                                     .ForEach(constraint =>
                                              value.AddConstraint(constraint));

                                 result.AddDecimalValue(value);
                             });

            return result;
        }
    }
}