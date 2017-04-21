using System;
using System.Data;

namespace Abacus.Data
{
    public static class MethodTestColumns
    {
        public static readonly QueryColumn<string> Description =
            new QueryColumn<string>("Description", DbType.AnsiString, 250);

        public static readonly QueryColumn<decimal> ExpectedResult =
            new QueryColumn<decimal>("ExpectedResult", DbType.Decimal);

        public static readonly QueryColumn<Guid> Id =
            new QueryColumn<Guid>("MethodTestId", DbType.Guid).AsIdentifier();

        public static readonly QueryColumn<Guid> MethodId =
            new QueryColumn<Guid>("MethodId", DbType.Guid);

        public static class ArgumentAnswerColumns
        {
            public static readonly QueryColumn<string>
                Answer = new QueryColumn<string>("Answer", DbType.AnsiString, 100);

            public static readonly QueryColumn<string>
                AnswerType = new QueryColumn<string>("AnswerType", DbType.AnsiString, 100);

            public static readonly QueryColumn<string>
                ArgumentName = new QueryColumn<string>("ArgumentName", DbType.AnsiString, 100);

            public static readonly QueryColumn<Guid> ArgumentId = new QueryColumn<Guid>("ArgumentId", DbType.Guid);

            public static readonly QueryColumn<Guid> MethodTestId =
                new QueryColumn<Guid>("MethodTestId", DbType.Guid).AsIdentifier();
        }
    }
}
