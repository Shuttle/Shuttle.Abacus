using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterMatrixElementCommand
    {
        public Guid MatrixId { get; set; }
        public Guid Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Value { get; set; }
    }
}