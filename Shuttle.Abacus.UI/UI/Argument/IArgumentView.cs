using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Argument
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
