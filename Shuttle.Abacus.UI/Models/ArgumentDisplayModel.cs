using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Models
{
    public class ArgumentDisplayModel
    {
        public ArgumentDisplayModel(IEnumerable<DataRow> argumentRows)
        {
            ArgumentRows = argumentRows;
        }

        public IEnumerable<DataRow> ArgumentRows { get; set; }

        public DataTable GraphNodeArguments { get; set; }

        public bool HasGraphNodeArguments
        {
            get { return GraphNodeArguments != null && GraphNodeArguments.Rows.Count > 0; }
        }

        public DataRow GetArgumentRow(Guid argumentId)
        {
            foreach (var row in ArgumentRows)
            {
                if (ArgumentColumns.Id.MapFrom(row).Equals(argumentId))
                {
                    return row;
                }
            }

            throw new MissingEntityException(string.Format("Argument id {0} not found.", argumentId));
        }
    }
}