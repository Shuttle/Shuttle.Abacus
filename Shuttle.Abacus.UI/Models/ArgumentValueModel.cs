using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Models
{
    public class ArgumentValueModel
    {
        public Guid ArgumentId { get; set; }
        public string Value { get; set; }

        public ArgumentValueModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            ArgumentId = ArgumentColumns.ValueColumns.ArgumentId.MapFrom(row);
            Value = ArgumentColumns.ValueColumns.Value.MapFrom(row);
        }

        public ArgumentValueModel()
        {
        }
    }
}