namespace Shuttle.Abacus
{
    public class AnswerTypeDTO
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public bool IsMapping
        {
            get { return !string.IsNullOrEmpty(Name) && Name.ToLower() == "mapping"; }
        }
    }
}
