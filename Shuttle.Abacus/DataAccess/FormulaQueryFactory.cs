using System;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaQueryFactory : IFormulaQueryFactory
    {
        private readonly string SelectClause = @"
select 
    Id,
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
    Id,
    SequenceNumber,
    Operation,
    ValueProviderName,
    InputParameter
from
    FormulaOperation
where
    Id = @Id
")
                .AddParameterValue(Columns.Id, id);
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    Id = @Id"))
                .AddParameterValue(Columns.Id, id);
        }

        public IQuery RemoveOperation(Guid operationId)
        {
            return
                RawQuery.Create("delete from FormulaOperation where OperationId = @Id")
                    .AddParameterValue(Columns.Id, operationId);
        }

        public IQuery RemoveConstraint(Guid constraintId)
        {
            return
                RawQuery.Create("delete from FormulaConstraint where ConstraintId = @Id")
                    .AddParameterValue(Columns.Id, constraintId);
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
    FormulaId = @Id
")
                .AddParameterValue(Columns.Id, id);
        }

        public IQuery Registered(Guid formulaId, string name)
        {
            return RawQuery.Create(@"
insert into Formula
(
    Id,
    Name
)
values
(
    @Id,
    @Name
)")
                .AddParameterValue(Columns.Id, formulaId)
                .AddParameterValue(Columns.Name, name);
        }

        public IQuery Remove(Guid formulaId)
        {
            return
                RawQuery.Create("delete from Formula where Id = @Id")
                    .AddParameterValue(Columns.Id, formulaId);
        }

        public IQuery Rename(Guid formulaId, string name)
        {
            return RawQuery.Create(@"
update 
    Formula 
set
    Name = @Name
where
    Id = @Id
")
                .AddParameterValue(Columns.Id, formulaId)
                .AddParameterValue(Columns.Name, name);
        }

        public IQuery AddOperation(Guid operationId, Guid formulaId, int sequenceNumber, string operation, string valueProviderName, string inputParameter)
        {
            return RawQuery.Create(@"
insert into FormulaOperation
(
    FormulaId,
    SequenceNumber,
    Operation,
    ValueProviderName,
    InputParameter
)
values
(
    @Id,
    @SequenceNumber,
    @Operation,
    @ValueProviderName,
    @InputParameter
)
")
                .AddParameterValue(Columns.Id, formulaId)
                .AddParameterValue(Columns.Operation, operation)
                .AddParameterValue(Columns.ValueProviderName, valueProviderName)
                .AddParameterValue(Columns.InputParameter, inputParameter)
                .AddParameterValue(Columns.SequenceNumber, sequenceNumber);
        }

        public IQuery Save(Formula item)
        {
            return RawQuery.Create(@"
update 
    Formula
set
    Name = @Name
where
    Id = @Id
")
                .AddParameterValue(Columns.Name, item.Name)
                .AddParameterValue(Columns.Id, item.Id);
        }

        public IQuery Search(FormulaSearchSpecification specification)
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
    and
(
    @MaximumFormulaName is null
    or
    @MaximumFormulaName = ''
    or
    MaximumFormulaName like '%' + @MaximumFormulaName + '%'
)
    and
(
    @MinimumFormulaName is null
    or
    @MinimumFormulaName = ''
    or
    MinimumFormulaName like '%' + @MinimumFormulaName + '%'
)
order by 
    Name
"))
                .AddParameterValue(Columns.Name, specification.Name)
                .AddParameterValue(Columns.MaximumFormulaName, specification.MaximumFormulaName)
                .AddParameterValue(Columns.MinimumFormulaName, specification.MinimumFormulaName);
        }

        public IQuery AddConstraint(Guid constraintId, Guid formulaId, string argumentName,
            string comparison,
            string value)
        {
            return RawQuery.Create(@"
insert into FormulaConstraint
(
    Id,
    FormulaId,
    ArgumentName,
    Comparison,
    Value
)
values
(
    @Id,
    @FormulaId,
    @SequenceNumber,
    @ArgumentName,
    @Comparison,
    @Value
)")
                .AddParameterValue(Columns.Id, constraintId)
                .AddParameterValue(Columns.FormulaId, formulaId)
                .AddParameterValue(Columns.ArgumentName, argumentName)
                .AddParameterValue(Columns.Comparison, comparison)
                .AddParameterValue(Columns.Value, value);
        }
    }
}