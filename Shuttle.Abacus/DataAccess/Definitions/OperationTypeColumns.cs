using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class OperationTypeColumns
    {
        public static readonly MappedColumn<string> Name = new MappedColumn<string>("Name", DbType.AnsiString, 100);
        public static readonly MappedColumn<string> Text = new MappedColumn<string>("Text", DbType.AnsiString, 100);
    }
}
