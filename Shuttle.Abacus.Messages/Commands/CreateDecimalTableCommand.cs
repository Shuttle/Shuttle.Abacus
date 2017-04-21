using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using Abacus.DTO;
using NServiceBus;

namespace Abacus.Messages
{
    public class CreateDecimalTableCommand : IMessage, ICreateDecimalTableCommand
    {
        public Guid DecimalTableId { get; set; }
        public string DecimalTableName { get; set; }
        public ArgumentDTO RowArgumentDto { get; set; }
        public ArgumentDTO ColumnArgumentDTO { get; set; }
        public List<DecimalValueDTO> DecimalValueDTOs { get; set; }
    }
}
