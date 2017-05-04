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
            "decimal", "integer", "money"
        };

        public DataRow Row { get; }

        public Guid Id => ArgumentColumns.Id.MapFrom(Row);
        public string Name => ArgumentColumns.Name.MapFrom(Row);
        public string AnswerType => ArgumentColumns.AnswerType.MapFrom(Row);

        public ArgumentModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            Row = row;
        }

        public bool IsText()
        {
            return AnswerType.Equals("Text", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsMoney()
        {
            return AnswerType.Equals("Money", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsNumber
        {
            get { return !string.IsNullOrEmpty(AnswerType) && Numbers.Contains(AnswerType.ToLower()); }
        }
    }
}