using System.Collections.Generic;
using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.Summary
{
    public interface ISummaryPresenter : IPresenter
    {
        void Populate(IEnumerable<NamedQueryResult> namedQueryResults);
    }
}
