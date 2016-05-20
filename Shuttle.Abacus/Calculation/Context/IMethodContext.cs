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
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IMethodContext : ICalculationDisplay<IMethodContext>,
                                       ICanBeReadOnly<IMethodContext>,
                                       IDisposable
    {
        IEnumerable<ICalculationResult> Results { get; }
        IEnumerable<ArgumentAnswer> ArgumentAnswers { get; }
        IEnumerable<string> ErrorMessages { get; }
        IEnumerable<string> WarningMessages { get; }
        IEnumerable<string> InformationMessages { get; }
        ICalculationResult Total { get; }
        bool LoggerEnabled { get; }
        string LogText { get; }
        bool OK { get; }
        IEnumerable<ICalculationResult> SubTotals { get; }
        List<GraphNodeDTO> GraphNodes();
        void AddErrorMessage(string message);
        void AddWarningMessage(string message);
        void AddInformationMessage(string message);
        ICalculationResult GetResult(string name);
        SubTotalCalculationResult GetSubTotal(string name);
        bool HasResult(string name);
        bool HasArgumentAnswer(string argumentName);
        IMethodContext AddArgumentAnswer(ArgumentAnswer argumentAnswer);
        ArgumentAnswer GetArgumentAnswer(string argumentName);
        void Log();
        void Log(string text, params string[] args);
        void IncreaseIndent();
        void DecreaseIndent();
        IMethodContext AddResult(ICalculationResult calculationResult);
        IMethodContext Wrapped(string title);
        IMethodContext Copy();

        void LogResults();
        void LogSubTotals();
        void IncrementSubTotal(ICalculationResult calculationResult);
    }
}
