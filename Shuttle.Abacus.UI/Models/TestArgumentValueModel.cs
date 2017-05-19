using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Models
{
    public class TestArgumentValueModel
    {
        public string ArgumentName { get; set; }
        public string Value { get; set; }

        public TestArgumentValueModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            ArgumentName = TestColumns.ArgumentValueColumns.ArgumentName.MapFrom(row);
            Value = TestColumns.ArgumentValueColumns.Value.MapFrom(row);
        }

        public TestArgumentValueModel()
        {
        }

        public List<ArgumentModel> Arguments { get; set; }
    }
}