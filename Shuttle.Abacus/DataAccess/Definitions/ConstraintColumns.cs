using System;
using System.Data;

namespace Abacus.Data
{
    public static class ConstraintColumns
    {
        public static readonly QueryColumn<Guid> OwnerId = new QueryColumn<Guid>("OwnerId", DbType.Guid);

        public static readonly QueryColumn<string> OwnerName = new QueryColumn<string>("OwnerName",
                                                                                               DbType.AnsiString, 100);

        public static readonly QueryColumn<string> Answer = new QueryColumn<string>("Answer", DbType.AnsiString, 120);

        public static readonly QueryColumn<string> AnswerType = new QueryColumn<string>("AnswerType", DbType.AnsiString,
                                                                                        100);

        public static readonly QueryColumn<string> Description = new QueryColumn<string>("Description",
                                                                                         DbType.AnsiString, 250);

        public static readonly QueryColumn<Guid> ArgumentId = new QueryColumn<Guid>("ArgumentId", DbType.Guid);

        public static readonly QueryColumn<string> ArgumentName = new QueryColumn<string>("ArgumentName", DbType.AnsiString,
                                                                                        120);

        public static readonly QueryColumn<string> Name = new QueryColumn<string>("Name", DbType.AnsiString, 60);

        public static readonly QueryColumn<int> SequenceNumber = new QueryColumn<int>("SequenceNumber", DbType.Int32);
    }
}
