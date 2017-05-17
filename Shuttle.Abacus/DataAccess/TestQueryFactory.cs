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

        public IQuery Add(Test item)
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
                .AddParameterValue(TestColumns.Id, item.Id)
                .AddParameterValue(TestColumns.Name, item.Name)
                .AddParameterValue(TestColumns.FormulaName, item.FormulaName)
                .AddParameterValue(TestColumns.ExpectedResult, item.ExpectedResult)
                .AddParameterValue(TestColumns.ExpectedResultType, item.ExpectedResultType)
                .AddParameterValue(TestColumns.Comparison, item.Comparison);
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

        //public  IQuery Get(Guid id)
        //{
        //    var query = SelectQuery.CreateSelectFrom(@"
        //            select
        //                mt.TestId,
        //                FormulaId,
        //                Description,
        //                ExpectedResult,
        //                ArgumentName,
        //                ArgumentName,
        //                ValueType,
        //                Answer
        //            from
        //                Test mt
        //                    inner join
        //                TestArgumentValue mtaa
        //                    on (mtaa.TestId = mt.TestId)
        //            where
        //                mt.TestId = @TestId
        //            ");

        //    query.AddParameterValue(TestColumns.Id, id);

        //    return query;
        //}
    }
}