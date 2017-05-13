using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.Resources;

namespace Shuttle.Abacus.UI.Messages.Core
{
    public class SummaryViewRequestedMessage : NullPermissionMessage
    {
        private readonly List<NamedQueryResult> namedQueryResults = new List<NamedQueryResult>();

        public SummaryViewRequestedMessage(Resource item, ResourceCollection relatedItems)
        {
            Item = item;
            RelatedItems = relatedItems;
        }

        public Resource Item { get; private set; }
        public ResourceCollection RelatedItems { get; private set; }

        public IEnumerable<NamedQueryResult> NamedQueryResults => new ReadOnlyCollection<NamedQueryResult>(namedQueryResults);

        public void AddTable(string name, IEnumerable<DataRow> rows)
        {
            namedQueryResults.Add(new NamedQueryResult(name, NamedQueryResult.DisplayType.Table, rows));
        }

        public void AddRow(string name, DataRow row)
        {
            namedQueryResults.Add(new NamedQueryResult(name, NamedQueryResult.DisplayType.Row, row != null ? new List<DataRow> { row } : new List<DataRow>()));
        }
    }
}