using System;

namespace Abacus.Data
{
    public static class ConstraintQueries
    {
        public const string TableName = "Constraint";

        public static ISelectQuery AllForOwner(Guid ownerId)
        {
            return SelectBuilder
                .Select(ConstraintColumns.Description)
                .Where(ConstraintColumns.OwnerId).EqualTo(ownerId)
                .OrderBy(ConstraintColumns.SequenceNumber).Ascending()
                .From(TableName);
        }

        public static ISelectQuery DTOsForOwner(Guid ownerId)
        {
            return SelectBuilder
                .Select(ConstraintColumns.Name)
                .With(ConstraintColumns.ArgumentId)
                .With(ConstraintColumns.ArgumentName)
                .With(ConstraintColumns.Answer)
                .Where(ConstraintColumns.OwnerId).EqualTo(ownerId)
                .OrderBy(ConstraintColumns.SequenceNumber).Ascending()
                .From(TableName);
        }
    }
}
