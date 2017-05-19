using System.Collections.Generic;

namespace Shuttle.Abacus.Shell.Models
{
    public class TestArgumentModel
    {
        public string ArgumentName { get; set; }
        public string Value { get; set; }

        public List<ArgumentModel> Arguments { get; set; }
    }
}