using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class TestQueryFactory : ITestQueryFactory
    {
        private const string SelectClause = @"
select
    TestId,
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
    TestArgumentValue
where
    TestId = @TestId
order by
    ArgumentName
")
                .AddParameterValue(TestColumns.Id, id);
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    TestId = @TestId
"))
                .AddParameterValue(TestColumns.Id, id);
        }

        public IQuery Remove(Guid id)
        {
            return
                RawQuery.Create("delete from Test where TestId = @TestId")
                    .AddParameterValue(TestColumns.Id, id);
        }

        public IQuery AddArgumentAnswer(Test test, TestArgumentValue argumentValue)
        {
            return RawQuery.Create(@"
insert into TestArgumentValue
(
    TestId,
    ArgumentName,
    Answer,
    ValueType,
    ArgumentName
)
values
(
    @TestId,
    @ArgumentName,
    @Answer,
    @ValueType,
    @ArgumentName
)")
                .AddParameterValue(TestColumns.Id, test.Id)
                .AddParameterValue(TestColumns.ArgumentValueColumns.ArgumentName, argumentValue.ArgumentId)
                .AddParameterValue(TestColumns.ArgumentValueColumns.Value, argumentValue.Answer);
        }

        public IQuery Register(Guid id, string name, string formulaName, string expectedResult, string expectedResultType,
            string comparison)
        {
            return RawQuery.Create(@"
insert into Test
(
    TestId,
    Name,
    FormulaName,
    ExpectedResult,
    ExpectedResultType,
    Comparison
)
values
(
    @TestId,
    @Name,
    @FormulaName,
    @ExpectedResult,
    @ExpectedResultType,
    @Comparison
)")
                .AddParameterValue(TestColumns.Id, id)
                .AddParameterValue(TestColumns.Name, name)
                .AddParameterValue(TestColumns.FormulaName, formulaName)
                .AddParameterValue(TestColumns.ExpectedResult, expectedResult)
                .AddParameterValue(TestColumns.ExpectedResultType, expectedResultType)
                .AddParameterValue(TestColumns.Comparison, comparison);
        }

        public IQuery RemoveArgumentValues(Guid id)
        {
            return
                RawQuery.Create("delete from TestArgumentValue where TestId = @TestId")
                    .AddParameterValue(TestColumns.Id, id);
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
    TestId = @TestId
")
                    .AddParameterValue(TestColumns.Id, id);
        }
    }
}