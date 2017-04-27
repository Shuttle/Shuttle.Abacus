using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class CalculationQueryFactory : ICalculationQueryFactory
    {
        private static string SelectClause = @"
select
    CalculationId,
    MethodId,
    OwnerName,
    OwnerId,
    Name,
    Type,
    Required,
    SequenceNumber
from
    Calculation
";

        public IQuery AllForOwner(Guid ownerId)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    OwnerId = @OwnerId
order by
    SequenceNumber
"))
                .AddParameterValue(CalculationColumns.OwnerId, ownerId);
        }

        public IQuery AllForMethod(Guid methodId)
        {
            return RawQuery.Create(@"
select
    CalculationId,
    Name
from
    Calculation
where
    MethodId = @MethodId
")
                .AddParameterValue(CalculationColumns.MethodId, methodId);
        }

        public IQuery AllBeforeCalculation(Guid methodId, Guid calculationId)
        {
            var query =
                RawQuery.Create(string.Concat(SelectClause,@"
where
    MethodId = @MethodId
and
    SequenceNumber <
                (select SequenceNumber
                from Calculation AS Calculation_SequenceNumber
                where (CalculationId = @CalculationId))"))
                    .AddParameterValue(CalculationColumns.MethodId, methodId)
                    .AddParameterValue(CalculationColumns.Id, calculationId);

            return query;
        }

        public IQuery Name(Guid id)
        {
            return RawQuery.Create(@"select Name from Calculation where CalculationId = @CalculationId")
                .AddParameterValue(CalculationColumns.Id, id);
        }

        public IQuery AllForMethod(Guid methodId, Guid grabberCalculationId)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    MethodId = @MethodId
and
    Id <> @Id
order by
    SequenceNumber
"))
                .AddParameterValue(CalculationColumns.MethodId, methodId)
                .AddParameterValue(CalculationColumns.Id, grabberCalculationId);
        }

        public IQuery GraphNodeArguments(Guid calculationId)
        {
            return RawQuery.Create(@"
select
    ArgumentId,
    Format
from
    GraphNodeArgument
where
    CalculationId = @CalculationId
order by
    SequenceNumber
")
                .AddParameterValue(GraphNodeArgumentColumns.CalculationId, calculationId);
        }

        public  IQuery Add(Method method, ICalculationOwner owner, Calculation item)
        {
            return RawQuery.Create(@"
insert into Calculation
(
    CalculationId,
    MethodId,
    OwnerName,
    OwnerId,
    Name,
    Type,
    Required,
    SequenceNumber
)
values
(
    @CalculationId,
    @MethodId,
    @OwnerName,
    @OwnerId,
    @Name,
    @Type,
    @Required,
    @SequenceNumber
)")
                .AddParameterValue(CalculationColumns.Id, item.Id)
                .AddParameterValue(CalculationColumns.MethodId, method.Id)
                .AddParameterValue(CalculationColumns.OwnerName, owner.OwnerName)
                .AddParameterValue(CalculationColumns.OwnerId, owner.Id)
                .AddParameterValue(CalculationColumns.Name, item.Name)
                .AddParameterValue(CalculationColumns.Type, item.Type)
                .AddParameterValue(CalculationColumns.Required, item.Required)
                .AddParameterValue(CalculationColumns.SequenceNumber, -1);
        }

        public  IQuery Remove(Calculation item)
        {
            return RawQuery.Create("delete from TABLE where Id = @Id").AddParameterValue(CalculationColumns.Id, item.Id);
        }

        public  IQuery Get(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    CalculationId = @CalculationId"))
                .AddParameterValue(CalculationColumns.Id, id);
        }

        public  IQuery Save(Calculation item)
        {
            return RawQuery.Create(@"
update Calculation set
    Name = @Name,
    Required = @Required
where
    CalculationId = @CalculationId")
                .AddParameterValue(CalculationColumns.Name, item.Name)
                .AddParameterValue(CalculationColumns.Required, item.Required)
                .AddParameterValue(CalculationColumns.Id, item.Id);
        }

        public  IQuery SetSequenceNumber(Guid id, int sequenceNumber)
        {
            return RawQuery.Create(@"
update Calculation set
    SequenceNumber = @SequenceNumber
where
    CalculationId = @CalculationId")
                .AddParameterValue(CalculationColumns.SequenceNumber, sequenceNumber)
                .AddParameterValue(CalculationColumns.Id, id);
        }

        public  IQuery Save(Calculation calculation, string ownerName, Guid ownerId, int sequence)
        {
            return RawQuery.Create(@"
update Calculation set
    OwnerName = @OwnerName,
    OwnerId = @OwnerId,
    SequenceNumber = @SequenceNumber
where
    CalculationId = @CalculationId")
                .AddParameterValue(CalculationColumns.OwnerName, ownerName)
                .AddParameterValue(CalculationColumns.OwnerId, ownerId)
                .AddParameterValue(CalculationColumns.SequenceNumber, sequence)
                .AddParameterValue(CalculationColumns.Id, calculation.Id);
        }
}
}