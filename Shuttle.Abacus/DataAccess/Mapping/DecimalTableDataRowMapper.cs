using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
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

        public MappedRow<DecimalTable> Map(DataRow row)
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

            return new MappedRow<DecimalTable>(row, result);
        }
    }
}