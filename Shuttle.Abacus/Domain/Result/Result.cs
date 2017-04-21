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
