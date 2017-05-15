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
    FormulaId,
    ExpectedResult,
    ExpectedResultType,
    Comparison,
    Description
from
    Test
";

        public IQuery All()
        {
            return RawQuery.Create(string.Concat(SelectClause, "order by Description"));
        }

        public IQuery AllForMethod(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    FormulaId = @FormulaId
order by 
    Description
"))
                .AddParameterValue(TestColumns.FormulaId, id);
        }

        public IQuery GetArgumentAnswers(Guid id)
        {
            return RawQuery.Create(@"
select
    TestId,
    ArgumentName,
    Answer,
    ValueType,
    ArgumentName
from
    TestArgumentValue
where
    TestId = @TestId
order by
    ArgumentName
")
                .AddParameterValue(TestColumns.ArgumentValueColumns.TestId, id);
        }

        public IQuery AllUsingArgument(Guid argumentId)
        {
            return RawQuery.Create(@"
select
    Description
from
    Test mt
inner join
    TestArgumentValue mtaa on
        (mtaa.TestId = mt.TestId)
where
    mtaa.ArgumentName = @ArgumentName
")
                .AddParameterValue(TestColumns.ArgumentValueColumns.ArgumentName, argumentId);
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
    FormulaId,
    ExpectedResult,
    Description
)
values
(
    @TestId,
    @FormulaId,
    @ExpectedResult,
    @Description
)")
                .AddParameterValue(TestColumns.Id, item.Id)
                .AddParameterValue(TestColumns.FormulaId, item.FormulaId)
                .AddParameterValue(TestColumns.Description, item.Description)
                .AddParameterValue(TestColumns.ExpectedResult, item.ExpectedResult);
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
                .AddParameterValue(TestColumns.ArgumentValueColumns.TestId, test.Id)
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