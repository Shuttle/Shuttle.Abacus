using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.MethodTest
{
    public interface IMethodTestView : IView
    {
        string DescriptionValue { get; set; }

        decimal ExpectedResultValue { get; set; }

        IList ArgumentAnswers { get; }

        ArgumentDTO ArgumentDto { get; }
        bool HasArgument { get; }
        bool HasAnswer { get; }
        string AnswerValue { get; }
        void EnableAnswerSelection();
        void EnableAnswerEntry();
        void PopulateAnswerCatalog(IEnumerable<ArgumentRestrictedAnswerDTO> list);
        void PopulateFactors(IEnumerable<ArgumentDTO> items);
        void ShowArgumentError();
        void ShowAnswerError(string message);
        void AddArgumentAnswer(Guid argumentId, string answer);
        void AddArgument(ArgumentDTO dto);

        IRuleCollection<object> DescriptionRules { set; }
        IRuleCollection<object> ExpectedResultRules { set; }
        bool HasInvalidArgumentAnswers();

        ComboBox ValueSelectionControl { get; }
        TextBox FormattedControl { get; }

        void AttachValueFormatter(MoneyFormatter formatter);
        void DetachValueFormatter();
    }
}
