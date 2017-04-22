using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class MethodColumns
    {
        public static readonly MappedColumn<Guid> Id = new MappedColumn<Guid>("MethodId", DbType.Guid);

        public static readonly MappedColumn<string> Name = new MappedColumn<string>("Name", DbType.AnsiString, 100);
    }
}
