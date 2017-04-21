using System;
using System.Linq;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class FormulaTableAccess
    {
        
        public const string OperationTableName = "FormulaOperation";

        public static IQuery Add(IFormulaOwner owner, Formula item)
        {
            return InsertBuilder.Insert()
                .AddParameterValue(FormulaColumns.Id, item.Id)
                .Add(FormulaColumns.OwnerName).WithValue(owner.OwnerName)
                .Add(FormulaColumns.OwnerId).WithValue(owner.Id)
                .Add(FormulaColumns.Description).WithValue(item.Description())
                .Add(FormulaColumns.SequenceNumber).WithValue(owner.Formulas.Count())
                .Into(TableName);
        }

        public static IQuery Remove(Formula item)
        {
            return RawQuery.Create("delete from TABLE where Id = @Id").AddParameterValue(FormulaColumns.Id, item.Id);
        }

        public static IQuery Get(Guid id)
        {
            return Get()
                .AddParameterValue(FormulaColumns.Id, id)
                .From(TableName);
        }

        private static ISelectBuilderSelect Get()
        {
            return RawQuery.Create(@"
select
                Id,
                OwnerName,
                OwnerId,
                Description,;
        }

        public static IQuery GetOperation(Guid formulaId)
        {
            return RawQuery.Create(@"
select
                Operation,
                ValueSource,
                ValueSelection,
                .AddParameterValue(FormulaOperationColumns.FormulaId, formulaId)
                .OrderBy(FormulaOperationColumns.SequenceNumber).Ascending()
                .From(OperationTableName);
        }

        public static IQuery RemoveOperations(Formula formula)
        {
            return DeleteBuilder.AddParameterValue(FormulaOperationColumns.FormulaId, formula.Id).From(OperationTableName);
        }

        public static IQuery AddOperation(Formula formula, FormulaOperation operation, int sequenceNumber)
        {
            var valueSelectionHolder = operation.ValueSource as IValueSelectionHolder;

            var valueSelection = valueSelectionHolder == null
                                     ? string.Empty
                                     : valueSelectionHolder.ValueSelection;

            return InsertBuilder
                .Insert()
                .Add(FormulaOperationColumns.FormulaId).WithValue(formula.Id)
                .Add(FormulaOperationColumns.Operation).WithValue(operation.Name)
                .Add(FormulaOperationColumns.ValueSource).WithValue(operation.ValueSource.Name)
                .Add(FormulaOperationColumns.ValueSelection).WithValue(valueSelection)
                .Add(FormulaOperationColumns.Text).WithValue(operation.ValueSource.Text)
                .Add(FormulaOperationColumns.SequenceNumber).WithValue(sequenceNumber)
                .Into(OperationTableName);
        }

        public static IQuery Save(Formula item)
        {
            return UpdateBuilder.Update(TableName)
                .Set(FormulaColumns.Description).ToValue(item.Description())
                .AddParameterValue(FormulaColumns.Id).HasValue(item.Id);
        }

        public static IQuery SetSequenceNumber(Formula formula, int sequence)
        {
            return UpdateBuilder
                .Update(TableName)
                .Set(FormulaColumns.SequenceNumber).ToValue(sequence)
                .AddParameterValue(FormulaColumns.Id).HasValue(formula.Id);
        }

        public static IQuery AllForOwner(Guid ownerId)
        {
            return Get()
               .AddParameterValue(FormulaColumns.OwnerId, ownerId)
               .From(TableName);
        }
    }
}
