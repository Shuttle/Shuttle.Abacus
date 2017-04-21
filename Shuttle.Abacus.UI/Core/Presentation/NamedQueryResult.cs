using Abacus.Data;

namespace Abacus.UI
{
    public class NamedQueryResult
    {
        public enum DisplayType
        {
            Table = 0,
            Row = 1
        }

        public NamedQueryResult(string name, DisplayType type, IQueryResult queryResult)
        {
            Name = name;
            Type = type;
            QueryResult = queryResult;
        }

        public string Name { get; private set; }
        public DisplayType Type { get; private set; }
        public IQueryResult QueryResult { get; private set; }
    }
}
