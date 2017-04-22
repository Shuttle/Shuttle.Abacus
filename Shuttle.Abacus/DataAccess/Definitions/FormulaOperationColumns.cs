using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class FormulaOperationColumns
    {
        public static readonly MappedColumn<Guid> FormulaId = new MappedColumn<Guid>("FormulaId", DbType.Guid);

        public static readonly MappedColumn<string> Operation = new MappedColumn<string>("Operation", DbType.AnsiString,
                                                                                       120);

        public static readonly MappedColumn<int> SequenceNumber = new MappedColumn<int>("SequenceNumber", DbType.Int32);

        public static readonly MappedColumn<string> ValueSelection = new MappedColumn<string>("ValueSelection",
                                                                                            DbType.AnsiString, 120);

        public static readonly MappedColumn<string> Text = new MappedColumn<string>("Text", DbType.AnsiString, 120);

        public static readonly MappedColumn<string> ValueSource = new MappedColumn<string>("ValueSource",
                                                                                         DbType.AnsiString, 120);
    }
}
