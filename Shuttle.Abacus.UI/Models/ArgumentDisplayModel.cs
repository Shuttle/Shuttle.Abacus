using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.UI.Models
{
    public class ArgumentDisplayModel
    {
        public ArgumentDisplayModel(IEnumerable<ArgumentDTO> factors)
        {
            Factors = factors;
        }

        public IEnumerable<ArgumentDTO> Factors { get; set; }

        public DataTable GraphNodeArguments { get; set; }

        public bool HasGraphNodeArguments
        {
            get { return GraphNodeArguments != null && GraphNodeArguments.Rows.Count > 0; }
        }

        public ArgumentDTO GetArgumentDTO(Guid argumentId)
        {
            foreach (var dto in Factors)
            {
                if (dto.Id.Equals(argumentId))
                {
                    return dto;
                }
            }

            throw new MissingEntryException(string.Format("Argument id {0} not found.", argumentId));
        }
    }
}
