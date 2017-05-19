using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Models
{
    public class ArgumentModel
    {
        private static readonly List<string> Numbers = new List<string>
        {
            "decimal",
            "integer",
            "money"
        };

        public ArgumentModel()
        {
        }

        public ArgumentModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            Id = ArgumentColumns.Id.MapFrom(row);
            Name = ArgumentColumns.Name.MapFrom(row);
            ValueType = ArgumentColumns.AnswerType.MapFrom(row);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ValueType { get; set; }

        public bool IsNumber()
        {
            return !string.IsNullOrEmpty(ValueType) && Numbers.Contains(ValueType.ToLower());
        }

        public bool IsText()
        {
            return ValueType.Equals("Text", StringComparison.OrdinalIgnoreCase);
        }
    }
}