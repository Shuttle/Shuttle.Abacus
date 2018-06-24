using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterMatrixCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RowArgumentName { get; set; }
        public string ColumnArgumentName { get; set; }
        public string DataTypeName { get; set; }
    }
}