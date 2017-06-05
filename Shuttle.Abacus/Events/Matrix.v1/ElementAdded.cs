namespace Shuttle.Abacus.Events.Matrix.v1
{
    public class ElementAdded
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string Value { get; set; }
    }
}