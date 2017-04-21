using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
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
