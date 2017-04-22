using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.UI.Models
{
    public class ConstraintModel
    {
        public IEnumerable<ConstraintTypeDTO> ConstraintTypes { get; set; }
        public IEnumerable<ArgumentDTO> Arguments { get; set; }
        public IEnumerable<ConstraintDTO> Constraints { get; set; }
    }
}
