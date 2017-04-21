using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.UI
{
    public class ConstraintModel
    {
        public IEnumerable<ConstraintTypeDTO> ConstraintTypes { get; set; }
        public IEnumerable<ArgumentDTO> Arguments { get; set; }
        public IEnumerable<ConstraintDTO> Constraints { get; set; }
    }
}
