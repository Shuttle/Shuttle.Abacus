using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Models
{
    public class ConstraintTypeModel
    {
        public ConstraintTypeModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            Row = row;
        }

        public string Name => ConstraintTypeColumns.Name.MapFrom(Row);
        public string Text => ConstraintTypeColumns.Text.MapFrom(Row);
        public bool EnabledForRestrictedAnswers => ConstraintTypeColumns.EnabledForRestrictedAnswers.MapFrom(Row);

        public DataRow Row { get; private set; }
    }
}