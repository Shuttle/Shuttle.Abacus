using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Models
{
    public class ArgumentModel
    {
        private static readonly List<string> Numbers = new List<string>
        {
            "decimal",
            "integer",
            "money"
        };

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AnswerType { get; set; }

        public bool IsNumber
        {
            get { return !string.IsNullOrEmpty(AnswerType) && Numbers.Contains(AnswerType.ToLower()); }
        }

        public ArgumentModel Using(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            Id = ArgumentColumns.Id.MapFrom(row);
            Name = ArgumentColumns.Name.MapFrom(row);
            AnswerType = ArgumentColumns.AnswerType.MapFrom(row);

            return this;
        }

        public bool IsText()
        {
            return AnswerType.Equals("Text", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsMoney()
        {
            return AnswerType.Equals("Money", StringComparison.OrdinalIgnoreCase);
        }
    }
}