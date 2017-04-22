using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public class NamedQueryResult
    {
        public enum DisplayType
        {
            Table = 0,
            Row = 1
        }

        public NamedQueryResult(string name, DisplayType type, IEnumerable<DataRow> rows)
        {
            Name = name;
            Type = type;
            Rows = rows;
        }

        public string Name { get; private set; }
        public DisplayType Type { get; private set; }
        public IEnumerable<DataRow> Rows { get; private set; }
    }
}
