using System;
using Abacus.Domain;

namespace Abacus.Data
{
    public class CalculationTableAccess
    {
        private const string TableName = "Calculation";

        public static IQuery Add(Method method, ICalculationOwner owner, Calculation item)
        {
            return InsertBuilder.Insert()
                .Add(CalculationColumns.Id).WithValue(item.Id)
                .Add(CalculationColumns.MethodId).WithValue(method.Id)
                .Add(CalculationColumns.OwnerName).WithValue(owner.OwnerName)
                .Add(CalculationColumns.OwnerId).WithValue(owner.Id)
                .Add(CalculationColumns.Name).WithValue(item.Name)
                .Add(CalculationColumns.Type).WithValue(item.Type)
                .Add(CalculationColumns.Required).WithValue(item.Required)
                .Add(CalculationColumns.SequenceNumber).WithValue(-1)
                .Into(TableName);
        }

        public static IQuery Remove(Calculation item)
        {
            return DeleteBuilder.Where(CalculationColumns.Id).EqualTo(item.Id).From(TableName);
        }

        public static IQuery Get(Guid id)
        {
            return Get()
                .Where(CalculationColumns.Id).EqualTo(id)
                .From(TableName);
        }

        private static ISelectBuilderSelect Get()
        {
            return SelectBuilder
                .Select(CalculationColumns.Id)
                .With(CalculationColumns.MethodId)
                .With(CalculationColumns.OwnerName)
                .With(CalculationColumns.OwnerId)
                .With(CalculationColumns.Name)
                .With(CalculationColumns.Type)
                .With(CalculationColumns.Required)
                .With(CalculationColumns.SequenceNumber);
        }

        public static IQuery Save(Calculation item)
        {
            return UpdateBuilder.Update(TableName)
                .Set(CalculationColumns.Name).ToValue(item.Name)
                .Set(CalculationColumns.Required).ToValue(item.Required)
                .Where(CalculationColumns.Id).HasValue(item.Id);
        }

        public static IQuery SetSequenceNumber(Guid id, int sequenceNumber)
        {
            return UpdateBuilder.Update(TableName)
                .Set(CalculationColumns.SequenceNumber).ToValue(sequenceNumber)
                .Where(CalculationColumns.Id).HasValue(id);
        }

        public static IQuery AllForOwner(Guid ownerId)
        {
            return Get()
                .Where(CalculationColumns.OwnerId).EqualTo(ownerId)
                .OrderBy(CalculationColumns.SequenceNumber).Ascending()
                .From(TableName);
        }

        public static IQuery Save(Calculation calculation, string ownerName, Guid ownerId, int sequence)
        {
            return UpdateBuilder.Update(TableName)
                .Set(CalculationColumns.OwnerName).ToValue(ownerName)
                .Set(CalculationColumns.OwnerId).ToValue(ownerId)
                .Set(CalculationColumns.SequenceNumber).ToValue(sequence)
                .Where(CalculationColumns.Id).HasValue(calculation.Id);
        }
    }
}
