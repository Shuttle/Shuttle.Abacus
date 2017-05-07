using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class FormulaColumns
    {
        public static readonly MappedColumn<Guid> Id = new MappedColumn<Guid>("FormulaId", DbType.Guid);
        public static readonly MappedColumn<string> Name = new MappedColumn<string>("Name", DbType.AnsiString, 120);
        public static readonly MappedColumn<string> MaximumFormulaName = new MappedColumn<string>("MaximumFormulaName", DbType.AnsiString, 120);
        public static readonly MappedColumn<string> MinimumFormulaName = new MappedColumn<string>("MinimumFormulaName", DbType.AnsiString, 120);
    }
}