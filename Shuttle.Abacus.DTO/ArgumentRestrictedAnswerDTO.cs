namespace Shuttle.Abacus.DTO
{
    public class ArgumentRestrictedAnswerDTO
    {
        public ArgumentRestrictedAnswerDTO(string answer)
        {
            Answer = answer;
        }

        public ArgumentRestrictedAnswerDTO()
        {
        }

        public string Answer { get; set; }
    }
}
