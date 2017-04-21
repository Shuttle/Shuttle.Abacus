using System;
using System.Data;

namespace Abacus.Data
{
    public static class FormulaOperationColumns
    {
        public static readonly QueryColumn<Guid> FormulaId = new QueryColumn<Guid>("FormulaId", DbType.Guid);

        public static readonly QueryColumn<string> Operation = new QueryColumn<string>("Operation", DbType.AnsiString,
                                                                                       120);

        public static readonly QueryColumn<int> SequenceNumber = new QueryColumn<int>("SequenceNumber", DbType.Int32);

        public static readonly QueryColumn<string> ValueSelection = new QueryColumn<string>("ValueSelection",
                                                                                            DbType.AnsiString, 120);

        public static readonly QueryColumn<string> Text = new QueryColumn<string>("Text", DbType.AnsiString, 120);

        public static readonly QueryColumn<string> ValueSource = new QueryColumn<string>("ValueSource",
                                                                                         DbType.AnsiString, 120);
    }
}
