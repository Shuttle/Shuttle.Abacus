using System;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class DecimalTableFactory : IDecimalTableFactory
    {
        private readonly IFactoryProvider<IConstraintFactory> constraintFactoryProvider;
        private readonly IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider;

        public DecimalTableFactory(IFactoryProvider<IConstraintFactory> constraintFactoryProvider, IFactoryProvider<IArgumentAnswerFactory> argumentAnswerFactoryProvider)
        {
            this.constraintFactoryProvider = constraintFactoryProvider;
            this.argumentAnswerFactoryProvider = argumentAnswerFactoryProvider;
        }

        public DecimalTable Create(Guid decimalTableId, DecimalTableCommand command)
        {
            var result = new DecimalTable(decimalTableId, command.DecimalTableName, command.RowArgumentId, command.ColumnArgumentId);

            throw new NotImplementedException();

            foreach (var dto in command.DecimalValues)
            {
                var value = new DecimalValue(Guid.NewGuid(), dto.Column, dto.Row, dto.Value);

                //TODO
                //foreach (var constraintDTO in dto.ConstraintDTOs)
                //{
                //    ////value.AddConstraint(
                //    ////    constraintFactoryProvider.Get(constraintDTO.ConstraintTypeDTO.Name).Create(constraintDTO.ArgumentDto.Id, argumentAnswerFactoryProvider.Get(constraintDTO.ArgumentDto.AnswerType).Create(constraintDTO.ArgumentDto.Name, constraintDTO.Value)));
                //}

                result.AddDecimalValue(value);
            }

            return result;
        }
    }
}
