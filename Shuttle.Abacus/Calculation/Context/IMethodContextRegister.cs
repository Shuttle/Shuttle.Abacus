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

using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IMethodContextRegister
    {
        IEnumerable<ICalculationResult> Results { get; }
        IEnumerable<ICalculationResult> SubTotals { get; }
        ICalculationResult Total { get; }
        ICalculationResult GetResult(string name);
        bool HasResult(string name);
        SubTotalCalculationResult GetSubTotal(string name);
        void AddResult(ICalculationResult calculationResult);
        void IncrementSubTotal(ICalculationResult calculationResult);
    }
}
