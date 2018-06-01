using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class ArgumentColumns
    {
        public static readonly MappedColumn<string>
            AnswerType = new MappedColumn<string>("ValueType", DbType.AnsiString);

        public static readonly MappedColumn<Guid> Id =
            new MappedColumn<Guid>("ArgumentId", DbType.Guid);

        public static readonly MappedColumn<string>
            Name = new MappedColumn<string>("Name", DbType.AnsiString);

        public static class ValueColumns
        {
            public static readonly MappedColumn<string>
                Value = new MappedColumn<string>("Value", DbType.AnsiString);

            public static readonly MappedColumn<Guid> ArgumentId =
                new MappedColumn<Guid>("ArgumentId", DbType.Guid);
        }
    }
}