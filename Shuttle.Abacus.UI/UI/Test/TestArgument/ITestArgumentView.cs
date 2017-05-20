using System.Collections.Generic;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.Test.TestArgument
{
    public interface ITestArgumentView : IView, IViewReady
    {
        ArgumentModel ArgumentModel { get; }
        string ArgumentName { get; set; }
        bool HasArgumentValue { get; }
        bool HasArgument { get; }
        string ValueValue { get; set; }
        void PopulateArguments(IEnumerable<ArgumentModel> items);
        void PopulateArgumentValues(IEnumerable<string> values);
        void ShowArgumentValueError(string message);
        void ShowArgumentError();
    }
}