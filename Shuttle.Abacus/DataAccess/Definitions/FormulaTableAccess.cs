using System;
using System.Linq;
using Abacus.Domain;

namespace Abacus.Data
{
    public class FormulaTableAccess
    {
        public const string TableName = "Formula";
        public const string OperationTableName = "FormulaOperation";

        public static IQuery Add(IFormulaOwner owner, Formula item)
        {
            return InsertBuilder.Insert()
                .Add(FormulaColumns.Id).WithValue(item.Id)
                .Add(FormulaColumns.OwnerName).WithValue(owner.OwnerName)
                .Add(FormulaColumns.OwnerId).WithValue(owner.Id)
                .Add(FormulaColumns.Description).WithValue(item.Description())
                .Add(FormulaColumns.SequenceNumber).WithValue(owner.Formulas.Count())
                .Into(TableName);
        }

        public static IQuery Remove(Formula item)
        {
            return DeleteBuilder.Where(FormulaColumns.Id).EqualTo(item.Id).From(TableName);
        }

        public static IQuery Get(Guid id)
        {
            return Get()
                .Where(FormulaColumns.Id).EqualTo(id)
                .From(TableName);
        }

        private static ISelectBuilderSelect Get()
        {
            return SelectBuilder
                .Select(FormulaColumns.Id)
                .With(FormulaColumns.OwnerName)
                .With(FormulaColumns.OwnerId)
                .With(FormulaColumns.Description);
        }

        public static IQuery GetOperation(Guid formulaId)
        {
            return SelectBuilder
                .Select(FormulaOperationColumns.Operation)
                .With(FormulaOperationColumns.ValueSource)
                .With(FormulaOperationColumns.ValueSelection)
                .Where(FormulaOperationColumns.FormulaId).EqualTo(formulaId)
                .OrderBy(FormulaOperationColumns.SequenceNumber).Ascending()
                .From(OperationTableName);
        }

        public static IQuery RemoveOperations(Formula formula)
        {
            return DeleteBuilder.Where(FormulaOperationColumns.FormulaId).EqualTo(formula.Id).From(OperationTableName);
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
                .Where(FormulaColumns.Id).HasValue(item.Id);
        }

        public static IQuery SetSequenceNumber(Formula formula, int sequence)
        {
            return UpdateBuilder
                .Update(TableName)
                .Set(FormulaColumns.SequenceNumber).ToValue(sequence)
                .Where(FormulaColumns.Id).HasValue(formula.Id);
        }

        public static IQuery AllForOwner(Guid ownerId)
        {
            return Get()
               .Where(FormulaColumns.OwnerId).EqualTo(ownerId)
               .From(TableName);
        }
    }
}
