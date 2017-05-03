using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IArgumentQueryFactory
    {
        IQuery All();
        IQuery Get(Guid id);
        IQuery GetRestrictedAnswer(Guid id);
        IQuery Add(Argument item);
        IQuery Remove(Guid id);
        IQuery GetDTO(Guid id);
        IQuery Save(Argument item);
        IQuery RemoveRestrictedAnswers(Argument argument);
        IQuery SaveRestrictedAnswers(Argument argument, string answer);
    }
}