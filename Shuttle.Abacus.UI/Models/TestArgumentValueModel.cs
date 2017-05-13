using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Models
{
    public class TestArgumentValueModel
    {
        public Guid ArgumentName { get; set; }
        public string Value { get; set; }

        public TestArgumentValueModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            ArgumentName = TestColumns.ArgumentValueColumns.ArgumentName.MapFrom(row);
            Value = TestColumns.ArgumentValueColumns.Value.MapFrom(row);
        }
    }
}