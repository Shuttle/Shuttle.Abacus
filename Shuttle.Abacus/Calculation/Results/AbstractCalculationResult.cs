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

namespace Shuttle.Abacus
{
    public abstract class AbstractCalculationResult : ICalculationResult
    {
        protected AbstractCalculationResult(Calculation calculation)
        {
            Calculation = calculation;
        }

        public Calculation Calculation { get; private set; }

        public string Name
        {
            get { return Calculation.Name; }
        }

        public decimal Value { get; protected set; }

        public abstract string Description();

        public abstract void Add(ICalculationResult result);

        public abstract void Limit(decimal value);
    }
}
