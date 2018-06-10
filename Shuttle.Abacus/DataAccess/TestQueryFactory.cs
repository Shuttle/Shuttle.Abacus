using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class TestQueryFactory : ITestQueryFactory
    {
        private const string SelectClause = @"
select
    Id,
    Name,
    FormulaName,
    ExpectedResult,
    ExpectedResultType,
    Comparison
from
    Test
";

        public IQuery All()
        {
            return RawQuery.Create(string.Concat(SelectClause, "order by Name"));
        }

        public IQuery ArgumentValues(Guid id)
        {
            return RawQuery.Create(@"
select
    TestId,
    ArgumentName,
    Value
from
    TestArgument
where
    TestId = @Id
order by
    ArgumentName
")
                .AddParameterValue(Columns.Id, id);
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    Id = @Id
"))
                .AddParameterValue(Columns.Id, id);
        }

        public IQuery Remove(Guid id)
        {
            return
                RawQuery.Create("delete from Test where Id = @Id")
                    .AddParameterValue(Columns.Id, id);
        }

        public IQuery Register(Guid id, string name, string formulaName, string expectedResult,
            string expectedResultType,
            string comparison)
        {
            return RawQuery.Create(@"
insert into Test
(
    Id,
    Name,
    FormulaName,
    ExpectedResult,
    ExpectedResultType,
    Comparison
)
values
(
    @Id,
    @Name,
    @FormulaName,
    @ExpectedResult,
    @ExpectedResultType,
    @Comparison
)")
                .AddParameterValue(Columns.Id, id)
                .AddParameterValue(Columns.Name, name)
                .AddParameterValue(Columns.FormulaName, formulaName)
                .AddParameterValue(Columns.ExpectedResult, expectedResult)
                .AddParameterValue(Columns.ExpectedResultType, expectedResultType)
                .AddParameterValue(Columns.Comparison, comparison);
        }

        public IQuery RemoveArgumentValues(Guid id)
        {
            return
                RawQuery.Create("delete from TestArgument where TestId = @Id")
                    .AddParameterValue(Columns.Id, id);
        }

        public IQuery Rename(Guid id, string name)
        {
            return
                RawQuery.Create(@"
update
    Test
set
    Name = @Name
where 
    Id = @Id
")
                    .AddParameterValue(Columns.Id, id);
        }

        public IQuery RemoveArgumentValue(Guid id, string argumentName)
        {
            return RawQuery.Create(
                    "delete from TestArgument where TestId = @Id and ArgumentName = @ArgumentName")
                .AddParameterValue(Columns.Id, id)
                .AddParameterValue(Columns.ArgumentName, argumentName);
        }

        public IQuery AddArgumentValue(Guid id, string argumentName, string value)
        {
            return RawQuery.Create(@"
insert into TestArgument 
(
    TestId,
    ArgumentName,
    Value
)
values
(
    @Id,
    @ArgumentName,
    @Value
)
")
                .AddParameterValue(Columns.Id, id)
                .AddParameterValue(Columns.ArgumentName, argumentName)
                .AddParameterValue(Columns.Value, value);
        }
    }
}