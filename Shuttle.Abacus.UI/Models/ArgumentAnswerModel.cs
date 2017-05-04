using System;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Models
{
    public class ArgumentAnswerModel
    {
        public Guid ArgumentId { get; set; }
        public string Argument { get; set; }
        public string Answer { get; set; }

        public ArgumentAnswerModel Using(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            ArgumentId = MethodTestColumns.ArgumentAnswerColumns.ArgumentId.MapFrom(row);
            Answer = MethodTestColumns.ArgumentAnswerColumns.Answer.MapFrom(row);

            return this;
        }
    }
}