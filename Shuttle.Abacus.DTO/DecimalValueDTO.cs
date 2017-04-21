using System.Collections.Generic;

namespace Shuttle.Abacus.DTO
{
    public class DecimalValueDTO
    {
        public DecimalValueDTO()
        {
            ConstraintDTOs = new List<ConstraintDTO>();
        }

        public int Row { get; set; }
        public int Column { get; set; }
        public decimal Value { get; set; }
        public List<ConstraintDTO> ConstraintDTOs { get; set; }
    }
}
