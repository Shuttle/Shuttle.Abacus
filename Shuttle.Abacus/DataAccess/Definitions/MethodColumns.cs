using System;
using System.Data;

namespace Abacus.Data
{
    public static class MethodColumns
    {
        public static readonly QueryColumn<Guid> Id = new QueryColumn<Guid>("MethodId", DbType.Guid).AsIdentifier();

        public static readonly QueryColumn<string> Name = new QueryColumn<string>("Name", DbType.AnsiString, 100);
    }
}
