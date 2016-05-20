/*
    This file forms part of Shuttle.Abacus.

    Shuttle.Abacus - A constraint-based calculation engine.
    Copyright (C) 2016  Eben Roux

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;

namespace Shuttle.Abacus
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
