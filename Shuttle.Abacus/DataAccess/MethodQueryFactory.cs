using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodQueryFactory : IMethodQueryFactory
    {
        private readonly string SelectClause = @"
select
    MethodId,
    Name
from
    Method
";

        public IQuery All()
        {
            return RawQuery.Create(string.Concat(SelectClause, "order by Name"));
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    MethodId = @MethodId
"))
                .AddParameterValue(MethodColumns.Id, id);
        }

        public IQuery Add(Method item)
        {
            return RawQuery.Create(@"
insert into Method
(
    MethodId,
    Name
)
values
(
    @MethodId,
    @Name
)")
                .AddParameterValue(MethodColumns.Id, item.Id)
                .AddParameterValue(MethodColumns.Name, item.MethodName);
        }

        public IQuery Remove(Guid id)
        {
            return RawQuery.Create("delete from Method where MethodId = @MethodId")
                .AddParameterValue(MethodColumns.Id, id);
        }

        public IQuery Save(Method item)
        {
            return RawQuery.Create("update Method set Name = @Name where MethodId = @MethodId")
                .AddParameterValue(MethodColumns.Name, item.MethodName)
                .AddParameterValue(MethodColumns.Id, item.Id);
        }

        public IQuery Get(string name)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    Name = @Name
"))
                .AddParameterValue(MethodColumns.Name, name);
        }
    }
}