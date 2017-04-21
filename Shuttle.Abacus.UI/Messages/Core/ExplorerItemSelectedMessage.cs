using System.Collections.Generic;
using System.Collections.ObjectModel;
using Abacus.Data;

namespace Abacus.UI
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

        public IEnumerable<NamedQueryResult> NamedQueryResults
        {
            get { return new ReadOnlyCollection<NamedQueryResult>(namedQueryResults); }
        }

        public void AddTable(string name, IQueryResult queryResult)
        {
            namedQueryResults.Add(new NamedQueryResult(name, NamedQueryResult.DisplayType.Table, queryResult));
        }

        public void AddRow(string name, IQueryResult queryResult)
        {
            namedQueryResults.Add(new NamedQueryResult(name, NamedQueryResult.DisplayType.Row, queryResult));
        }
    }
}
