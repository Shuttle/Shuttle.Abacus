using System;
using Shuttle.Core.Contract;
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
    ExpectedResultDataTypeName,
    Comparison
from
    Test
";

        public IQuery All()
        {
            return RawQuery.Create(string.Concat(SelectClause, "order by Name"));
        }

        public IQuery Arguments(Guid id)
        {
            return RawQuery.Create(@"
select
    ta.TestId,
    ta.ArgumentId,
    a.Name ArgumentName,
    ta.Value
from
    TestArgument ta
inner join
    Argument a on a.Id = ta.ArgumentId
where
    TestId = @Id
order by
    a.Name
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
            string expectedResultDataTypeName, string comparison)
        {
            return RawQuery.Create(@"
insert into Test
(
    Id,
    Name,
    FormulaName,
    ExpectedResult,
    ExpectedResultDataTypeName,
    Comparison
)
values
(
    @Id,
    @Name,
    @FormulaName,
    @ExpectedResult,
    @ExpectedResultDataTypeName,
    @Comparison
)")
                .AddParameterValue(Columns.Id, id)
                .AddParameterValue(Columns.Name, name)
                .AddParameterValue(Columns.FormulaName, formulaName)
                .AddParameterValue(Columns.ExpectedResult, expectedResult)
                .AddParameterValue(Columns.ExpectedResultDataTypeName, expectedResultDataTypeName)
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

        public IQuery RemoveArgumentValue(Guid id, Guid argumentId)
        {
            return RawQuery.Create(
                    "delete from TestArgument where TestId = @Id and ArgumentName = @ArgumentName")
                .AddParameterValue(Columns.Id, id)
                .AddParameterValue(Columns.ArgumentName, argumentId);
        }

        public IQuery AddArgumentValue(Guid id, Guid argumentId, string value)
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
                .AddParameterValue(Columns.ArgumentName, argumentId)
                .AddParameterValue(Columns.Value, value);
        }

        public IQuery Search(TestSearchSpecification specification)
        {
            Guard.AgainstNull(specification, nameof(specification));

            return RawQuery.Create(string.Concat(SelectClause, @"
where
(
    @Name is null
    or
    @Name = ''
    or
    Name like '%' + @Name + '%'
)
order by 
    Name
"))
                .AddParameterValue(Columns.Name, specification.Name);
        }
    }
}