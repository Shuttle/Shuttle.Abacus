using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Models
{
    public class ValueSourceTypeModel
    {
        public ValueSourceTypeModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            Name = ValueSourceTypeColumns.Name.MapFrom(row);
            Text = ValueSourceTypeColumns.Text.MapFrom(row);
            Type = ValueSourceTypeColumns.Type.MapFrom(row);
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }

        public bool IsSelection => Type.Equals("Selection", StringComparison.InvariantCultureIgnoreCase);
    }
}