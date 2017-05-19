using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Shell.Models
{
    public class FormulaViewModel
    {
        public FormulaViewModel()
        {
            Id = Guid.Empty;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<string> OperationTypes { get; set; }
        public IEnumerable<ValueSourceTypeModel> ValueSourceTypes { get; set; }
        public IEnumerable<FormulaOperationModel> FormulaOperations { get; set; }
        public IEnumerable<ArgumentModel> Arguments { get; set; }
    }
}