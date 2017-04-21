using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class CalculationQueryFactory
    {
        public IQuery AllForOwner(Guid ownerId)
        {
            return RawQuery.Create(@"
select
    Id,
    Type,
    Name,
    OwnerName
from
    Calculation
where
    OwnerId = @OwnerId
order by
    SequenceNumber
")
                .AddParameterValue(CalculationColumns.OwnerId, ownerId);
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(@"
select
    Id,
    Type,
    Name,
    OwnerName,
    Required
from
    Calculation
")
                .AddParameterValue(CalculationColumns.Id, id);
        }

        public IQuery AllForMethod(Guid methodId)
        {
            return RawQuery.Create(@"
select
    Id,
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
                RawQuery.Create(
                        @"
                select 
                    CalculationId,
                    Name
                from
                    Calculation
                where
                    MethodId = @MethodId
                and
                    SequenceNumber <
                             (select SequenceNumber
                              from Calculation AS Calculation_SequenceNumber
                              where (CalculationId = @CalculationId)) ")
                    .AddParameterValue(CalculationColumns.MethodId, methodId)
                    .AddParameterValue(CalculationColumns.Id, calculationId);

            return query;
        }

        public IQuery Name(Guid id)
        {
            return RawQuery.Create(@"select Name from Calculation where Id = @Id")
                .AddParameterValue(CalculationColumns.Id, id);
        }

        public IQuery AllForMethod(Guid methodId, Guid grabberCalculationId)
        {
            return RawQuery.Create(@"
select
    Id,
    Name
from
    Calculation
where
    MethodId = @MethodId
and
    Id <> @Id
order by
    SequenceNumber
")
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

        public static IQuery Add(Method method, ICalculationOwner owner, Calculation item)
        {
            return InsertBuilder.Insert()
                .AddParameterValue(CalculationColumns.Id, item.Id)
                .Add(CalculationColumns.MethodId).WithValue(method.Id)
                .Add(CalculationColumns.OwnerName).WithValue(owner.OwnerName)
                .Add(CalculationColumns.OwnerId).WithValue(owner.Id)
                .AddParameterValue(CalculationColumns.Name, item.Name)
                .AddParameterValue(CalculationColumns.Type, item.Type)
                .AddParameterValue(CalculationColumns.Required, item.Required)
                .Add(CalculationColumns.SequenceNumber).WithValue(-1)
                .Into(TableName);
        }

        public static IQuery Remove(Calculation item)
        {
            return RawQuery.Create("delete from TABLE where Id = @Id").AddParameterValue(CalculationColumns.Id, item.Id);
        }

        public static IQuery Get(Guid id)
        {
            return Get()
                .AddParameterValue(CalculationColumns.Id, id)
                .From(TableName);
        }

        private static ISelectBuilderSelect Get()
        {
            return RawQuery.Create(@"
select
                Id,
                MethodId,
                OwnerName,
                OwnerId,
                Name,
                Type,
                Required,
                SequenceNumber,;
        }

        public static IQuery Save(Calculation item)
        {
            return UpdateBuilder.Update(TableName)
                .Set(CalculationColumns.Name).ToValue(item.Name)
                .Set(CalculationColumns.Required).ToValue(item.Required)
                .AddParameterValue(CalculationColumns.Id).HasValue(item.Id);
        }

        public static IQuery SetSequenceNumber(Guid id, int sequenceNumber)
        {
            return UpdateBuilder.Update(TableName)
                .Set(CalculationColumns.SequenceNumber).ToValue(sequenceNumber)
                .AddParameterValue(CalculationColumns.Id).HasValue(id);
        }

        public static IQuery AllForOwner(Guid ownerId)
        {
            return Get()
                .AddParameterValue(CalculationColumns.OwnerId, ownerId)
                .OrderBy(CalculationColumns.SequenceNumber).Ascending()
                .From(TableName);
        }

        public static IQuery Save(Calculation calculation, string ownerName, Guid ownerId, int sequence)
        {
            return UpdateBuilder.Update(TableName)
                .Set(CalculationColumns.OwnerName).ToValue(ownerName)
                .Set(CalculationColumns.OwnerId).ToValue(ownerId)
                .Set(CalculationColumns.SequenceNumber).ToValue(sequence)
                .AddParameterValue(CalculationColumns.Id).HasValue(calculation.Id);
        }
}
}