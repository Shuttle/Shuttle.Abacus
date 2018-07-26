using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class Columns
    {
        public static readonly MappedColumn<Guid> ArgumentId = new MappedColumn<Guid>("ArgumentId", DbType.Guid);
        public static readonly MappedColumn<string> ArgumentName = new MappedColumn<string>("ArgumentName", DbType.AnsiString);
        public static readonly MappedColumn<string> Axis = new MappedColumn<string>("Axis", DbType.AnsiString);
        public static readonly MappedColumn<int> Column = new MappedColumn<int>("Column", DbType.Int32);
        public static readonly MappedColumn<Guid> ColumnArgumentId = new MappedColumn<Guid>("ColumnArgumentId", DbType.Guid);
        public static readonly MappedColumn<string> Comparison = new MappedColumn<string>("Comparison", DbType.AnsiString);
        public static readonly MappedColumn<string> DataTypeName = new MappedColumn<string>("DataTypeName", DbType.AnsiString);
        public static readonly MappedColumn<string> ExecutionType = new MappedColumn<string>("ExecutionType", DbType.AnsiString);
        public static readonly MappedColumn<string> ExpectedResult = new MappedColumn<string>("ExpectedResult", DbType.AnsiString);
        public static readonly MappedColumn<string> ExpectedResultType = new MappedColumn<string>("ExpectedResultType", DbType.AnsiString);
        public static readonly MappedColumn<Guid> FormulaId = new MappedColumn<Guid>("FormulaId", DbType.Guid);
        public static readonly MappedColumn<string> FormulaName = new MappedColumn<string>("FormulaName", DbType.AnsiString);
        public static readonly MappedColumn<Guid> Id = new MappedColumn<Guid>("Id", DbType.Guid);
        public static readonly MappedColumn<string> InputParameter = new MappedColumn<string>("InputParameter", DbType.AnsiString);
        public static readonly MappedColumn<string> MaximumFormulaName = new MappedColumn<string>("MaximumFormulaName", DbType.AnsiString);
        public static readonly MappedColumn<string> MinimumFormulaName = new MappedColumn<string>("MinimumFormulaName", DbType.AnsiString);
        public static readonly MappedColumn<string> Name = new MappedColumn<string>("Name", DbType.AnsiString);
        public static readonly MappedColumn<string> Operation = new MappedColumn<string>("Operation", DbType.AnsiString);
        public static readonly MappedColumn<int> Row = new MappedColumn<int>("Row", DbType.Int32);
        public static readonly MappedColumn<Guid> RowArgumentId = new MappedColumn<Guid>("RowArgumentId", DbType.Guid);
        public static readonly MappedColumn<int> SequenceNumber = new MappedColumn<int>("SequenceNumber", DbType.Int32);
        public static readonly MappedColumn<string> Value = new MappedColumn<string>("Value", DbType.AnsiString);
        public static readonly MappedColumn<string> ValueProviderName = new MappedColumn<string>("ValueProviderName", DbType.AnsiString);
    }
}