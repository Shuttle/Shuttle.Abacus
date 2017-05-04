using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.MethodTest
{
    public interface IMethodTestView : IView
    {
        string DescriptionValue { get; set; }

        decimal ExpectedResultValue { get; set; }

        IEnumerable<ArgumentAnswerModel> ArgumentAnswers { get; }

        ArgumentModel ArgumentModel { get; }
        bool HasArgument { get; }
        bool HasAnswer { get; }
        string AnswerValue { get; }

        IRuleCollection<object> DescriptionRules { set; }
        IRuleCollection<object> ExpectedResultRules { set; }

        ComboBox ValueSelectionControl { get; }
        TextBox FormattedControl { get; }
        void EnableAnswerSelection();
        void EnableAnswerEntry();
        void PopulateAnswers(IEnumerable<string> answers);
        void PopulateArguments(IEnumerable<ArgumentModel> arguments);
        void ShowArgumentError();
        void ShowAnswerError(string message);
        void AddArgumentAnswer(ArgumentModel argument, string answer);
        void AddArgument(ArgumentModel model);
        bool HasInvalidArgumentAnswers();

        void AttachValueFormatter(MoneyFormatter formatter);
        void DetachValueFormatter();
    }
}