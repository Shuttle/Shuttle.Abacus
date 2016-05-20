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
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class Result : AbstractResult, IResult
    {
        public static IResult Create()
        {
            return new Result(); 
        }

        public IResult AddFailureMessage(ResultMessage message)
        {
            FailureMessages.Add(message);

            return this;
        }

        public IResult AddFailureMessage(string message)
        {
            AddFailureMessages(message);

            return this;
        }

        public IResult AddFailureMessages(params string[] messages)
        {
            AddFailureMessages((IEnumerable<string>)messages);

            return this;
        }

        public IResult AddFailureMessages(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                FailureMessages.Add(mapper.MapFrom(message));
            }

            return this;
        }

        public IResult AddSuccessMessage(ResultMessage message)
        {
            SuccessMessages.Add(message);

            return this;
        }

        public IResult AddSuccessMessage(string message)
        {
            AddSuccessMessages(message);

            return this;
        }

        public IResult AddSuccessMessages(params string[] messages)
        {
            AddSuccessMessages((IEnumerable<string>)messages);

            return this;
        }

        public IResult AddSuccessMessages(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                SuccessMessages.Add(mapper.MapFrom(message));
            }

            return this;
        }

        public IResult AddException(Exception ex)
        {
            AddFailureMessages(ex.Messages());

            return this;
        }
    }

    public class Result<TValue> : AbstractResult, IResult<TValue>
    {
        public Result(TValue value)
        {
            Value = value;
        }

        public static IResult<TValue> Create()
        {
            return new Result<TValue>(default(TValue));
        }

        public IResult<TValue> AddFailureMessage(ResultMessage message)
        {
            FailureMessages.Add(message);

            return this;
        }

        public IResult<TValue> AddFailureMessage(string message)
        {
            AddFailureMessages(message);

            return this;
        }

        public IResult<TValue> AddFailureMessages(params string[] messages)
        {
            AddFailureMessages((IEnumerable<string>)messages);

            return this;
        }

        public IResult<TValue> AddFailureMessages(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                FailureMessages.Add(mapper.MapFrom(message));
            }

            return this;
        }

        public IResult<TValue> AddSuccessMessage(ResultMessage message)
        {
            SuccessMessages.Add(message);

            return this;
        }

        public IResult<TValue> AddSuccessMessage(string message)
        {
            AddSuccessMessages(message);

            return this;
        }

        public IResult<TValue> AddSuccessMessages(params string[] messages)
        {
            AddSuccessMessages((IEnumerable<string>)messages);

            return this;
        }

        public IResult<TValue> AddSuccessMessages(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                SuccessMessages.Add(mapper.MapFrom(message));
            }

            return this;
        }

        public IResult<TValue> AddException(Exception ex)
        {
            AddFailureMessages(ex.Messages());

            return this;
        }

        public TValue Value { get; private set; }
    }
}
