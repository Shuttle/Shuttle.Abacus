using System;

namespace Abacus.Data
{
    public class FormulaQueries
    {
        public const string OperationTableName = "FormulaOperation";
        public const string TableName = "Formula";

        public static ISelectQuery AllForOwner(Guid ownerId)
        {
            return SelectBuilder
                .Select(FormulaColumns.Id)
                .With(FormulaColumns.Description)
                .Where(FormulaColumns.OwnerId).EqualTo(ownerId)
                .From(TableName);
        }


        public static ISelectQuery GetOperations(Guid id)
        {
            return SelectBuilder
                .Select(FormulaOperationColumns.Operation)
                .With(FormulaOperationColumns.ValueSource)
                .With(FormulaOperationColumns.ValueSelection)
                .With(FormulaOperationColumns.Text)
                .Where(FormulaOperationColumns.FormulaId).EqualTo(id)
                .From(OperationTableName);
        }

        public static ISelectQuery Description(Guid id)
        {
            return SelectBuilder
                .Select(FormulaColumns.Description)
                .Where(FormulaColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static ISelectQuery Get(Guid id)
        {
            return SelectBuilder
                .Select(FormulaColumns.OwnerName)
                .With(FormulaColumns.OwnerId)
                .Where(FormulaColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static ISelectQuery OperationsSummary(Guid id)
        {
            return SelectBuilder
                .Select(FormulaOperationColumns.Operation)
                .With(FormulaOperationColumns.ValueSource)
                .With(FormulaOperationColumns.Text)
                .Where(FormulaOperationColumns.FormulaId).EqualTo(id)
                .From(OperationTableName);
        }
    }
}
