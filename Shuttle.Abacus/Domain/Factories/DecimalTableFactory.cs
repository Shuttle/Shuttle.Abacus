using System;

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

        public DecimalTable Create(Guid decimalTableId, IDecimalTableCommand command)
        {
            var result = new DecimalTable(decimalTableId, command.DecimalTableName, command.RowArgumentDto.Id, command.ColumnArgumentDTO.Id);

            foreach (var dto in command.DecimalValueDTOs)
            {
                var value = new DecimalValue(Guid.NewGuid(), dto.Column, dto.Row, dto.Value);

                foreach (var constraintDTO in dto.ConstraintDTOs)
                {
                    value.AddConstraint(
                        constraintFactoryProvider.Get(constraintDTO.ConstraintTypeDTO.Name).Create(constraintDTO.ArgumentDto.Id, argumentAnswerFactoryProvider.Get(constraintDTO.ArgumentDto.AnswerType).Create(constraintDTO.ArgumentDto.Name, constraintDTO.Value)));
                }

                result.AddDecimalValue(value);
            }

            return result;
        }
    }
}
