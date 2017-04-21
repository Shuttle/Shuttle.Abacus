namespace Shuttle.Abacus.DTO
{
    public class ValueSourceTypeDTO
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }

        public bool IsSelection
        {
            get { return Type == "Selection"; }
        }
    }
}
