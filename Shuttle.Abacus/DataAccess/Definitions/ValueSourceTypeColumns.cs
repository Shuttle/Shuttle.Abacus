using System.Data;

namespace Abacus.Data
{
    public static class ValueSourceTypeColumns
    {
        public static readonly QueryColumn<string> Name = new QueryColumn<string>("Name", DbType.AnsiString, 100);
        public static readonly QueryColumn<string> Text = new QueryColumn<string>("Text", DbType.AnsiString, 100);
        public static readonly QueryColumn<string> Type = new QueryColumn<string>("Type", DbType.AnsiString, 100);
    }
}
