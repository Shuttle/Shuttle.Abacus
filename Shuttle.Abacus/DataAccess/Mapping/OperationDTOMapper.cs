using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;

namespace Shuttle.Abacus.DataAccess.Mapping
{
    public class OperationDTOMapper : IDataRowMapper<OperationDTO>
    {
        private readonly List<OperationTypeDTO> operationTypes;
        private readonly List<ValueSourceTypeDTO> valueSourceTypes;

        public OperationDTOMapper(IOperationTypeQuery operationTypeQuery, IValueSourceTypeQuery valueSourceTypeQuery)
        {
            operationTypes = new List<OperationTypeDTO>(operationTypeQuery.AllDTOs());
            valueSourceTypes = new List<ValueSourceTypeDTO>(valueSourceTypeQuery.AllDTOs());
        }

        public IEnumerable<OperationDTO> MapFrom(DataTable input)
        {
            var result = new List<OperationDTO>();

            foreach (DataRow row in input.Rows)
            {
                var operation = FormulaOperationColumns.Operation.MapFrom(row);
                var valueSource = FormulaOperationColumns.ValueSource.MapFrom(row);

                var operationType =
                    operationTypes.Find(item => item.Name.Equals(operation, StringComparison.InvariantCultureIgnoreCase));

                var valueSourceType =
                    valueSourceTypes.Find(
                        item => item.Name.Equals(valueSource, StringComparison.InvariantCultureIgnoreCase));

                result.Add(new OperationDTO
                           {
                               OperationType = new OperationTypeDTO
                                               {
                                                   Name = operation,
                                                   Text = operationType != null
                                                              ? operationType.Text
                                                              : operation
                                               },
                               ValueSourceType = new ValueSourceTypeDTO
                                                 {
                                                     Name = valueSource,
                                                     Text = valueSourceType != null
                                                                ? valueSourceType.Text
                                                                : valueSource
                                                 },
                               ValueSelection = FormulaOperationColumns.ValueSelection.MapFrom(row),
                               Text = FormulaOperationColumns.Text.MapFrom(row)
                           });
            }

            return result;
        }
    }
}
