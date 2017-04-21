using System;
using System.Data;

namespace Abacus.Data
{
    public static class ArgumentColumns
    {
        public static readonly QueryColumn<string>
            AnswerType = new QueryColumn<string>("AnswerType", DbType.AnsiString, 100);

        public static readonly QueryColumn<Guid> Id =
            new QueryColumn<Guid>("ArgumentId", DbType.Guid).AsIdentifier();

        public static readonly QueryColumn<bool> IsSystemData = new QueryColumn<bool>("IsSystemData", DbType.Int16);

        public static readonly QueryColumn<string>
            Name = new QueryColumn<string>("Name", DbType.AnsiString, 100);

        public static class RestrictedAnswerColumns
        {
            public static readonly QueryColumn<string>
                Answer = new QueryColumn<string>("Answer", DbType.AnsiString, 100);

            public static readonly QueryColumn<Guid> ArgumentId =
                new QueryColumn<Guid>("ArgumentId", DbType.Guid).AsIdentifier();
        }
    }
}
