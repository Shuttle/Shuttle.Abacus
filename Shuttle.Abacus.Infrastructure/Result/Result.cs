using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public class Result : AbstractResult, IResult
    {
        public Result()
        {
            Headers = new List<ResultHeader>();
        }

        public List<ResultHeader> Headers { get; }

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
            AddFailureMessages((IEnumerable<string>) messages);

            return this;
        }

        public IResult AddFailureMessages(IEnumerable<string> messages)
        {
            FailureMessages.AddRange(messages.MapAllUsing(mapper));

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
            AddSuccessMessages((IEnumerable<string>) messages);

            return this;
        }

        public IResult AddSuccessMessages(IEnumerable<string> messages)
        {
            SuccessMessages.AddRange(messages.MapAllUsing(mapper));

            return this;
        }

        public IResult AddException(Exception ex)
        {
            AddFailureMessages(ex.Messages());

            return this;
        }

        public static IResult Create()
        {
            return new Result();
        }
    }

    public class Result<TValue> : AbstractResult, IResult<TValue>
    {
        public Result(TValue value)
        {
            Value = value;
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
            AddFailureMessages((IEnumerable<string>) messages);

            return this;
        }

        public IResult<TValue> AddFailureMessages(IEnumerable<string> messages)
        {
            FailureMessages.AddRange(messages.MapAllUsing(mapper));

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
            AddSuccessMessages((IEnumerable<string>) messages);

            return this;
        }

        public IResult<TValue> AddSuccessMessages(IEnumerable<string> messages)
        {
            SuccessMessages.AddRange(messages.MapAllUsing(mapper));

            return this;
        }

        public IResult<TValue> AddException(Exception ex)
        {
            AddFailureMessages(ex.Messages());

            return this;
        }

        public TValue Value { get; }

        public static IResult<TValue> Create()
        {
            return new Result<TValue>(default(TValue));
        }
    }
}