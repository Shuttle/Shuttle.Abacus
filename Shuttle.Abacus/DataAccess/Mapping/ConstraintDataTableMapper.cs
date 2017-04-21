using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess.Mapping
{
    public class ConstraintDataTableMapper : IDataRowMapper<IConstraint>
    {
        private readonly IFactoryProvider<IConstraintFactory> constraintFactoryProvider;
        private readonly IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider;

        public ConstraintDataTableMapper(IFactoryProvider<IConstraintFactory> constraintFactoryProvider, IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider)
        {
            this.constraintFactoryProvider = constraintFactoryProvider;
            this.argumentAnswerFactoryProvider = argumentAnswerFactoryProvider;
        }

        public IEnumerable<IConstraint> MapFrom(DataTable input)
        {
            var result = new List<IConstraint>();

            foreach (DataRow row in input.Rows)
            {
                result.Add(
                    constraintFactoryProvider.Get(ConstraintColumns.Name.MapFrom(row)).Create(ConstraintColumns.ArgumentId.MapFrom(row), argumentAnswerFactoryProvider.Get(ConstraintColumns.AnswerType.MapFrom(row)).Create(ConstraintColumns.ArgumentName.MapFrom(row), ConstraintColumns.Answer.MapFrom(row))));
            }

            return result;
        }
    }
}
