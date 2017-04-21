using System.Collections.Generic;
using Abacus.DTO;
using Abacus.Validation;

namespace Abacus.UI
{
    public interface IArgumentView : IView
    {
        string ArgumentNameValue{get; set;}
        string AnswerTypeValue { get; set; }
        IRuleCollection<object> ArgumentNameRules { set; }
        IRuleCollection<object> AnswerTypeRules { set; }
        bool HasValueType { get; }
        AnswerTypeDTO AnswerTypeDTO { get; }
        void PopulateAnswerTypes(IEnumerable<AnswerTypeDTO> items);
    }
}
