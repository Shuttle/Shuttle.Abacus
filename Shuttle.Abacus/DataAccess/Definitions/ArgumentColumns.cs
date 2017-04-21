using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class ArgumentColumns
    {
        public static readonly MappedColumn<string>
            AnswerType = new MappedColumn<string>("AnswerType", DbType.AnsiString, 100);

        public static readonly MappedColumn<Guid> Id =
            new MappedColumn<Guid>("ArgumentId", DbType.Guid);

        public static readonly MappedColumn<bool> IsSystemData = new MappedColumn<bool>("IsSystemData", DbType.Int16);

        public static readonly MappedColumn<string>
            Name = new MappedColumn<string>("Name", DbType.AnsiString, 100);

        public static class RestrictedAnswerColumns
        {
            public static readonly MappedColumn<string>
                Answer = new MappedColumn<string>("Answer", DbType.AnsiString, 100);

            public static readonly MappedColumn<Guid> ArgumentId =
                new MappedColumn<Guid>("ArgumentId", DbType.Guid);
        }
    }
}
