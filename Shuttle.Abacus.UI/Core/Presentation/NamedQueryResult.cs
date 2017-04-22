namespace Shuttle.Abacus.UI.Core.Presentation
{
    public class NamedQueryResult
    {
        public enum DisplayType
        {
            Table = 0,
            Row = 1
        }

        public NamedQueryResult(string name, DisplayType type, IEnumerable<DataRow> queryResult)
        {
            Name = name;
            Type = type;
            QueryResult = queryResult;
        }

        public string Name { get; private set; }
        public DisplayType Type { get; private set; }
        public IEnumerable<DataRow> QueryResult { get; private set; }
    }
}
