using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class ConstraintColumns
    {
        public static readonly MappedColumn<Guid> OwnerId = new MappedColumn<Guid>("OwnerId", DbType.Guid);

        public static readonly MappedColumn<string> OwnerName = new MappedColumn<string>("OwnerName",
                                                                                               DbType.AnsiString, 100);

        public static readonly MappedColumn<string> Answer = new MappedColumn<string>("Answer", DbType.AnsiString, 120);

        public static readonly MappedColumn<string> AnswerType = new MappedColumn<string>("AnswerType", DbType.AnsiString,
                                                                                        100);

        public static readonly MappedColumn<string> Description = new MappedColumn<string>("Description",
                                                                                         DbType.AnsiString, 250);

        public static readonly MappedColumn<Guid> ArgumentId = new MappedColumn<Guid>("ArgumentName", DbType.Guid);

        public static readonly MappedColumn<string> ArgumentName = new MappedColumn<string>("ArgumentName", DbType.AnsiString,
                                                                                        120);

        public static readonly MappedColumn<string> Name = new MappedColumn<string>("Name", DbType.AnsiString, 60);

        public static readonly MappedColumn<int> SequenceNumber = new MappedColumn<int>("SequenceNumber", DbType.Int32);
    }
}
