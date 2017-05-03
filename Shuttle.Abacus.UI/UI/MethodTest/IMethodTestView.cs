using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.MethodTest
{
    public interface IMethodTestView : IView
    {
        string DescriptionValue { get; set; }

        decimal ExpectedResultValue { get; set; }

        IList ArgumentAnswers { get; }

        DataRow ArgumentRow { get; }
        bool HasArgument { get; }
        bool HasAnswer { get; }
        string AnswerValue { get; }

        IRuleCollection<object> DescriptionRules { set; }
        IRuleCollection<object> ExpectedResultRules { set; }

        ComboBox ValueSelectionControl { get; }
        TextBox FormattedControl { get; }
        void EnableAnswerSelection();
        void EnableAnswerEntry();
        void PopulateAnswerCatalog(IEnumerable<string> answers);
        void PopulateFactors(IEnumerable<DataRow> items);
        void ShowArgumentError();
        void ShowAnswerError(string message);
        void AddArgumentAnswer(Guid argumentId, string answer);
        void AddArgument(DataRow dto);
        bool HasInvalidArgumentAnswers();

        void AttachValueFormatter(MoneyFormatter formatter);
        void DetachValueFormatter();
    }
}