using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaQueryFactory : IFormulaQueryFactory
    {
        public const string OperationTableName = "FormulaOperation";
        private readonly string SelectClause = @"
select 
    FormulaId,
    Name,
    MaximumFormulaName,
    MinimumFormulaName
from
    Formula
";


        public IQuery GetOperations(Guid id)
        {
            return RawQuery.Create(@"
select
    FormulaId,
    Operation,
    ValueSource,
    ValueSelection,
    SequenceNumber,
    Text
from
    FormulaOperation
")
                .AddParameterValue(FormulaOperationColumns.FormulaId, id);
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    FormulaId = @FormulaId"))
                .AddParameterValue(FormulaColumns.Id, id);
        }

        public IQuery Add(Formula formula)
        {
            return RawQuery.Create(@"
insert into Formula
(
    FormulaId,
    Name
)
values
(
    @FormulaId,
    @Name
)")
                .AddParameterValue(FormulaColumns.Id, formula.Id)
                .AddParameterValue(FormulaColumns.Name, formula.Name);
        }

        public IQuery Remove(Guid id)
        {
            return
                RawQuery.Create("delete from Formula where FormulaId = @FormulaId")
                    .AddParameterValue(FormulaColumns.Id, id);
        }

        public IQuery RemoveOperations(Formula formula)
        {
            return
                RawQuery.Create("delete from FormulaOperation where FormulaId = @FormulaId")
                    .AddParameterValue(FormulaColumns.Id, formula.Id);
        }

        public IQuery AddOperation(Formula formula, FormulaOperation operation, int sequenceNumber)
        {
            return RawQuery.Create(@"
insert into FormulaOperation
(
    FormulaId,
    Operation,
    ValueSource,
    ValueSelection,
    SequenceNumber,
    Text
)
values
(
    @FormulaId,
    @Operation,
    @ValueSource,
    @ValueSelection,
    @SequenceNumber,
    @Text
)
")
                .AddParameterValue(FormulaOperationColumns.FormulaId, formula.Id)
                .AddParameterValue(FormulaOperationColumns.Operation, operation)
                .AddParameterValue(FormulaOperationColumns.ValueSource, operation.ValueSource)
                .AddParameterValue(FormulaOperationColumns.ValueSelection, operation.ValueSelection)
                .AddParameterValue(FormulaOperationColumns.Text, operation.ValueSource)
                .AddParameterValue(FormulaOperationColumns.SequenceNumber, sequenceNumber);
        }

        public IQuery Save(Formula item)
        {
            return RawQuery.Create(@"
update 
    Formula
set
    Name = @Name
where
    FormulaId = @FormulaId
")
                .AddParameterValue(FormulaColumns.Name, item.Name)
                .AddParameterValue(FormulaColumns.Id, item.Id);
        }

        public IQuery All()
        {
            return RawQuery.Create(SelectClause);
        }
    }
}