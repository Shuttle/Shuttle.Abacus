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
    public interface IResult : IAbstractResult
    {
        IResult AddFailureMessage(ResultMessage message);
        IResult AddFailureMessage(string message);
        IResult AddFailureMessages(params string[] messages);
        IResult AddFailureMessages(IEnumerable<string> messages);
        IResult AddSuccessMessage(ResultMessage message);
        IResult AddSuccessMessage(string message);
        IResult AddSuccessMessages(params string[] messages);
        IResult AddSuccessMessages(IEnumerable<string> messages);
        IResult AddException(Exception ex);
    }

    public interface IResult<TValue> : IAbstractResult
    {
        TValue Value { get; }

        IResult<TValue> AddFailureMessage(ResultMessage message);
        IResult<TValue> AddFailureMessage(string message);
        IResult<TValue> AddFailureMessages(params string[] messages);
        IResult<TValue> AddFailureMessages(IEnumerable<string> messages);
        IResult<TValue> AddSuccessMessage(ResultMessage message);
        IResult<TValue> AddSuccessMessage(string message);
        IResult<TValue> AddSuccessMessages(params string[] messages);
        IResult<TValue> AddSuccessMessages(IEnumerable<string> messages);
        IResult<TValue> AddException(Exception ex);
    }
}
