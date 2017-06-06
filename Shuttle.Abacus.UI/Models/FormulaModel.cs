using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Models
{
    public class FormulaModel
    {
        public FormulaModel()
        {
            Id = Guid.Empty;
        }

        public FormulaModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            Id = FormulaColumns.FormulaId.MapFrom(row);
            Name = FormulaColumns.Name.MapFrom(row);
            MaximumFormulaName = FormulaColumns.MaximumFormulaName.MapFrom(row);
            MinimumFormulaName = FormulaColumns.MinimumFormulaName.MapFrom(row);
            ExecutionType = FormulaColumns.ExecutionType.MapFrom(row);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public string ExecutionType { get; set; }
        public string MaximumFormulaName { get; set; }
        public string MinimumFormulaName { get; set; }
    }
}