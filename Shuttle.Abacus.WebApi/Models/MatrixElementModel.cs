using System;

namespace Shuttle.Abacus.WebApi
{
    public class MatrixElementModel
    {
        public Guid Id { get; set; }
        public Guid MatrixId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Value { get; set; }
    }
}