using System;

namespace Shuttle.Abacus.Events.Matrix.v1
{
    public class ElementRegistered
    {
        public Guid Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Value { get; set; }
    }
}