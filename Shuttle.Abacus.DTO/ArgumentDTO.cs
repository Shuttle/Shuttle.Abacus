using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.DTO
{
    public class ArgumentDTO
    {
        private static readonly List<string> Numbers = new List<string>
                                                       {
                                                           "decimal", "integer", "money"
                                                       };

        public ArgumentDTO()
        {
            Answers = new List<ArgumentRestrictedAnswerDTO>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AnswerType { get; set; }

        public List<ArgumentRestrictedAnswerDTO> Answers { get; set; }

        public bool IsNumber
        {
            get { return !string.IsNullOrEmpty(AnswerType) && Numbers.Contains(AnswerType.ToLower()); }
        }

        public bool HasAnswerCatalog
        {
            get { return Answers != null && Answers.Count > 0; }
        }

        public bool IsMoney
        {
            get { return AnswerType.ToLower() == "money"; }
        }

        public bool IsText
        {
            get { return AnswerType.ToLower() == "text"; }
        }

        public bool CanOnlyCompareEquality
        {
            get { return HasAnswerCatalog || IsText; }
        }

        public bool HasArgumentName(string name)
        {
            return Answers.Find(item => item.Answer.Equals(name, StringComparison.InvariantCultureIgnoreCase)) != null;
        }
    }
}
