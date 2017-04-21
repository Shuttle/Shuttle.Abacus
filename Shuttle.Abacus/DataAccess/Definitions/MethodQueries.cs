using System;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class MethodQueries
    {
        

        public IQuery All()
        {
            return RawQuery.Create(@"
select
                Id,
                Name,
                .OrderBy(MethodColumns.Name).Ascending()
                .From(TableName);
        }

        public IQuery MethodName(Guid id)
        {
            return RawQuery.Create(@"
select
                Id,
                Name,
                .AddParameterValue(MethodColumns.Id, id)
                .From(TableName);
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(@"
select
                Id,
                Name,
                .AddParameterValue(MethodColumns.Id, id)
                .From(TableName);
        }
    }
}
