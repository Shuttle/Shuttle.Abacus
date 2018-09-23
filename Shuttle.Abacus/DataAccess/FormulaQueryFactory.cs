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
    o.Id,
    o.FormulaId,
    o.SequenceNumber,
    o.Operation,
    o.ValueProviderName,
    o.InputParameter,
    case ValueProviderName
        when 'Constant' then 
			o.InputParameter
        when 'Argument' then 
			(select Name from Argument where Id = o.InputParameter)
        when 'Matrix' then 
			(select Name from Matrix where Id = o.InputParameter)
        when 'Formula' then 
			(select Name from Formula where Id = o.InputParameter)
        else 
			'RunningTotal'
    end InputParameterDescription
from
    FormulaOperation o
where
    o.FormulaId = @Id
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
                RawQuery.Create("delete from FormulaOperation where Id = @Id")
                    .AddParameterValue(Columns.Id, operationId);
        }

        public IQuery RemoveConstraint(Guid constraintId)
        {
            return
                RawQuery.Create("delete from FormulaConstraint where Id = @Id")
                    .AddParameterValue(Columns.Id, constraintId);
        }

        public IQuery Constraints(Guid id)
        {
            return RawQuery.Create(@"
select
    c.Id,
    c.FormulaId,
    c.ArgumentId,
    a.Name ArgumentName,
    c.Comparison,
    c.Value
from
    FormulaConstraint c
inner join
    Argument a on a.Id = c.ArgumentId
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

        public IQuery RenumberOperations(Guid formulaId, int fromSequenceNumber)
        {
            return RawQuery.Create(@"
update 
    FormulaOperation
set
    SequenceNumber = SequenceNumber - 1
where
    FormulaId = @FormulaId
and
    SequenceNumber > @SequenceNumber
")
                .AddParameterValue(Columns.FormulaId, formulaId)
                .AddParameterValue(Columns.SequenceNumber, fromSequenceNumber);
        }

        public IQuery AddOperation(Guid operationId, Guid formulaId, int sequenceNumber, string operation, string valueProviderName, string inputParameter)
        {
            return RawQuery.Create(@"
insert into FormulaOperation
(
    Id,
    FormulaId,
    SequenceNumber,
    Operation,
    ValueProviderName,
    InputParameter
)
values
(
    @Id,
    @FormulaId,
    @SequenceNumber,
    @Operation,
    @ValueProviderName,
    @InputParameter
)
")
                .AddParameterValue(Columns.Id, operationId)
                .AddParameterValue(Columns.FormulaId, formulaId)
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

        public IQuery AddConstraint(Guid constraintId, Guid formulaId, Guid argumentId,
            string comparison,
            string value)
        {
            return RawQuery.Create(@"
insert into FormulaConstraint
(
    Id,
    FormulaId,
    ArgumentId,
    Comparison,
    Value
)
values
(
    @Id,
    @FormulaId,
    @ArgumentId,
    @Comparison,
    @Value
)")
                .AddParameterValue(Columns.Id, constraintId)
                .AddParameterValue(Columns.FormulaId, formulaId)
                .AddParameterValue(Columns.ArgumentId, argumentId)
                .AddParameterValue(Columns.Comparison, comparison)
                .AddParameterValue(Columns.Value, value);
        }
    }
}