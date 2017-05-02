using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Models
{
    public class FormulaModel
    {
        private readonly DataRow _row;

        public FormulaModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            _row = row;
        }

        public Guid Id => FormulaColumns.Id.MapFrom(_row);
        public int SequenceNumber => FormulaColumns.SequenceNumber.MapFrom(_row);
        public string Description => FormulaColumns.Description.MapFrom(_row);
    }
}