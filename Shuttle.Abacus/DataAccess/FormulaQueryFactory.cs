using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaQueryFactory : IFormulaQueryFactory
    {
        private readonly string SelectClause = @"
select 
    FormulaId,
    Name,
    MaximumFormulaName,
    MinimumFormulaName
from
    Formula
";

        public IQuery Operations(Guid id)
        {
            return RawQuery.Create(@"
select
    FormulaId,
    SequenceNumber,
    Operation,
    ValueSource,
    ValueSelection
from
    FormulaOperation
where
    FormulaId = @FormulaId
")
                .AddParameterValue(FormulaColumns.Id, id);
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
            throw new NotImplementedException();
        }

        public IQuery RemoveOperations(Guid formulaId)
        {
            return
                RawQuery.Create("delete from FormulaOperation where FormulaId = @FormulaId")
                    .AddParameterValue(FormulaColumns.Id, formulaId);
        }

        public IQuery RemoveConstraints(Guid formulaId)
        {
            return
                RawQuery.Create("delete from FormulaConstraint where FormulaId = @FormulaId")
                    .AddParameterValue(FormulaColumns.Id, formulaId);
        }

        public IQuery Constraints(Guid id)
        {
            return RawQuery.Create(@"
select
    FormulaId,
    SequenceNumber,
    ArgumentName,
    Comparison,
    Value
from
    FormulaConstraint
where
    FormulaId = @FormulaId
")
                .AddParameterValue(FormulaColumns.Id, id);
        }

        public IQuery Registered(Guid formulaId, string name)
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
                .AddParameterValue(FormulaColumns.Id, formulaId)
                .AddParameterValue(FormulaColumns.Name, name);
        }

        public IQuery Remove(Guid formulaId)
        {
            return
                RawQuery.Create("delete from Formula where FormulaId = @FormulaId")
                    .AddParameterValue(FormulaColumns.Id, formulaId);
        }

        public IQuery Renamed(Guid formulaId, string name)
        {
            return RawQuery.Create(@"
update 
    Formula 
set
    Name = @Name
where
    FormulaId = @FormulaId
")
                .AddParameterValue(FormulaColumns.Id, formulaId)
                .AddParameterValue(FormulaColumns.Name, name);
        }

        public IQuery AddOperation(Guid formulaId, int sequenceNumber, string operation, string valueSource,
            string valueSelection)
        {
            return RawQuery.Create(@"
insert into FormulaOperation
(
    FormulaId,
    SequenceNumber,
    Operation,
    ValueSource,
    ValueSelection
)
values
(
    @FormulaId,
    @SequenceNumber,
    @Operation,
    @ValueSource,
    @ValueSelection
)
")
                .AddParameterValue(FormulaColumns.Id, formulaId)
                .AddParameterValue(FormulaColumns.OperationColumns.Operation, operation)
                .AddParameterValue(FormulaColumns.OperationColumns.ValueSource, valueSource)
                .AddParameterValue(FormulaColumns.OperationColumns.ValueSelection, valueSelection)
                .AddParameterValue(FormulaColumns.OperationColumns.SequenceNumber, sequenceNumber);
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

        public IQuery AddConstraint(Guid formulaId, int sequenceNumber, string argumentName, string comparison, string value)
        {
            return RawQuery.Create(@"
insert into FormulaConstraint
(
    FormulaId,
    SequenceNumber,
    ArgumentName,
    Comparison,
    Value
)
values
(
    @FormulaId,
    @SequenceNumber,
    @ArgumentName,
    @Comparison,
    @Value
)")
                .AddParameterValue(FormulaColumns.Id, formulaId)
                .AddParameterValue(FormulaColumns.ConstraintColumns.SequenceNumber, sequenceNumber)
                .AddParameterValue(FormulaColumns.ConstraintColumns.ArgumentName, argumentName)
                .AddParameterValue(FormulaColumns.ConstraintColumns.Comparison, comparison)
                .AddParameterValue(FormulaColumns.ConstraintColumns.Value, value);
        }
    }
}