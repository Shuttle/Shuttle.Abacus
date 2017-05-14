using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Events.Formula.v1;
using Shuttle.Core.Data;
using Shuttle.Recall;

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
    SequenceNumber,
    Operation,
    ValueSource,
    ValueSelection
from
    FormulaOperation
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

        public IQuery RemoveConstraints(Formula formula)
        {
            return
                RawQuery.Create("delete from FormulaConstraint where FormulaId = @FormulaId")
                    .AddParameterValue(FormulaColumns.Id, formula.Id);
        }

        public IQuery GetConstraints(Guid id)
        {
            return RawQuery.Create(@"
select
    FormulaId,
    SequenceNumber,
    ArgumentName,
    ComparisonType,
    Value
from
    FormulaConstraint
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

        public IQuery AddOperation(Guid formulaId, int sequenceNumber, string operation, string valueSource, string valueSelection)
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

        public IQuery AddConstraint(Formula formula, FormulaConstraint constraint)
        {
            return RawQuery.Create(@"
insert into Constraint
(
    FormulaId,
    SequenceNumber
    ArgumentName,
    ComparisonType,
    Value
)
values
(
    @FormulaId,
    @SequenceNumber
    @ArgumentName,
    @ComparisonType,
    @Value
)")
                .AddParameterValue(FormulaColumns.Id, formula.Id)
                .AddParameterValue(FormulaColumns.ConstraintColumns.SequenceNumber, constraint.SequenceNumber)
                .AddParameterValue(FormulaColumns.ConstraintColumns.ArgumentName, constraint.ArgumentName)
                .AddParameterValue(FormulaColumns.ConstraintColumns.ComparisonType, constraint.ComparisonType)
                .AddParameterValue(FormulaColumns.ConstraintColumns.Value, constraint.Value);
        }
    }
}