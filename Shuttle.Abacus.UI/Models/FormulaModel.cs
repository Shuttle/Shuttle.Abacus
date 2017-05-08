using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Models
{
    public class FormulaModel
    {
        public FormulaModel()
        {
            Id = Guid.Empty;
        }

        public FormulaModel Using(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            Id = FormulaColumns.Id.MapFrom(row);
            Name = FormulaColumns.Name.MapFrom(row);

            return this;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<string> OperationTypes { get; set; }
        public IEnumerable<ValueSourceTypeModel> ValueSourceTypes { get; set; }
        public IEnumerable<FormulaOperationModel> FormulaOperations { get; set; }
        public IEnumerable<ArgumentModel> Arguments { get; set; }

        public string OperationType { get; set; }
        public string ValueSourceType { get; set; }
        public string ValueSelection { get; set; }
        public string Text { get; set; }
    }
}