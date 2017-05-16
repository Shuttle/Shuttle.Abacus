using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Test
{
    public interface ITestArgumentView : IView
    {
        ArgumentModel ArgumentModel { get; }
        string ArgumentValue { get; set; }
        bool HasArgumentValue { get; }
        bool HasArgument { get; }
        void PopulateArguments(IEnumerable<ArgumentModel> items);
        void PopulateArgumentValues(IEnumerable<string> values);
        void ShowArgumentValueError(string message);
        void ShowArgumentError();
    }
}