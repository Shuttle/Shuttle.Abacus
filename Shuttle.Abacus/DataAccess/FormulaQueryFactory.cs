using System;
using System.Linq;
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
    OwnerName,
    OwnerId,
    SequenceNumber,
    Description
from
    Formula
";


        public IQuery AllForOwner(Guid ownerId)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    OwnerId = @OwnerId
"))
                .AddParameterValue(FormulaColumns.OwnerId, ownerId);
        }

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

        public IQuery Add(string ownerName, Guid ownerId, Formula formula)
        {
            return RawQuery.Create(@"
insert into Formula
(
    FormulaId,
    OwnerName,
    OwnerId,
    SequenceNumber,
    Description
)
values
(
    @FormulaId,
    @OwnerName,
    @OwnerId,
    @SequenceNumber,
    @Description
)")
                .AddParameterValue(FormulaColumns.Id, formula.Id)
                .AddParameterValue(FormulaColumns.OwnerName, ownerName)
                .AddParameterValue(FormulaColumns.OwnerId, ownerId)
                .AddParameterValue(FormulaColumns.Description, formula.Description())
                .AddParameterValue(FormulaColumns.SequenceNumber, formula.SequenceNumber);
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
    Description = @Description
where
    FormulaId = @FormulaId
")
                .AddParameterValue(FormulaColumns.Description, item.Description())
                .AddParameterValue(FormulaColumns.Id, item.Id);
        }

        public IQuery SetSequenceNumber(Formula formula, int sequence)
        {
            return RawQuery.Create(@"
update 
    Formula
set
    SequenceNumber = @SequenceNumber
where
    FormulaId = @FormulaId
")
                .AddParameterValue(FormulaColumns.SequenceNumber, sequence)
                .AddParameterValue(FormulaColumns.Id, formula.Id);
        }
    }
}