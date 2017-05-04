using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Models
{
    public class FormulaModel
    {
        public FormulaModel With(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            Id = FormulaColumns.Id.MapFrom(row);
            SequenceNumber = FormulaColumns.SequenceNumber.MapFrom(row);
            Description = FormulaColumns.Description.MapFrom(row);

            return this;
        }

        public Guid Id { get; set; }
        public int SequenceNumber { get; set; }
        public string Description { get; set; }

        public IEnumerable<string> OperationTypes { get; set; }
        public IEnumerable<ValueSourceTypeModel> ValueSourceTypes { get; set; }
        public IEnumerable<FormulaOperationModel> FormulaOperations { get; set; }
        public IEnumerable<ArgumentModel> Arguments { get; set; }
    }
}