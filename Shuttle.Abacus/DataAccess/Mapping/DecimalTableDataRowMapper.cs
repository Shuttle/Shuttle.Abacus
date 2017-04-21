using System.Data;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess.Mapping
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