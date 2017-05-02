using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.UI.Models
{
    public class ConstraintModel
    {
        public IEnumerable<DataRow> ConstraintTypeRows { get; set; }
        public IEnumerable<DataRow> ArgumentRows { get; set; }
        public IEnumerable<ConstraintDTO> Constraints { get; set; }
    }
}
