using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentDataRowMapper : IDataRowMapper<Argument>
    {
        public MappedRow<Argument> Map(DataRow row)
        {
            return new MappedRow<Argument>(row, new Argument(ArgumentColumns.Id.MapFrom(row))
            {
                Name = ArgumentColumns.Name.MapFrom(row),
                AnswerType = ArgumentColumns.AnswerType.MapFrom(row),
                IsSystemData = ArgumentColumns.IsSystemData.MapFrom(row)
            });
        }
    }
}