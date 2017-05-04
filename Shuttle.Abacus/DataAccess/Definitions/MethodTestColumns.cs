using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class MethodTestColumns
    {
        public static readonly MappedColumn<string> Description =
            new MappedColumn<string>("Description", DbType.AnsiString, 250);

        public static readonly MappedColumn<decimal> ExpectedResult =
            new MappedColumn<decimal>("ExpectedResult", DbType.Decimal);

        public static readonly MappedColumn<Guid> Id =
            new MappedColumn<Guid>("MethodTestId", DbType.Guid);

        public static readonly MappedColumn<Guid> MethodId =
            new MappedColumn<Guid>("MethodId", DbType.Guid);

        public static class ArgumentAnswerColumns
        {
            public static readonly MappedColumn<string>
                Answer = new MappedColumn<string>("Answer", DbType.AnsiString, 100);

            public static readonly MappedColumn<Guid> ArgumentId = new MappedColumn<Guid>("ArgumentId", DbType.Guid);

            public static readonly MappedColumn<Guid> MethodTestId =
                new MappedColumn<Guid>("MethodTestId", DbType.Guid);
        }
    }
}
