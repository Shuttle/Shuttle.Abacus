using System;

namespace Shuttle.Abacus.DTO
{
    public class ArgumentAnswerDTO
    {
        public Guid ArgumentId { get; set; }
        public string ArgumentName { get; set; }
        public string AnswerType { get; set; }
        public string Answer { get; set; }
    }
}
