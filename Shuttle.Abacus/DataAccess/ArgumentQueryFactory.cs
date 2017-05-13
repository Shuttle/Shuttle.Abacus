using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Events.Argument.v1;
using Shuttle.Core.Data;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentQueryFactory : IArgumentQueryFactory
    {
        private readonly string SelectClause = @"
select
    ArgumentId,
    Name,
    AnswerType
from
    Argument
";
        private readonly string SelectClauseDTO = @"
select
    a.ArgumentId,
    Name,
    AnswerType,
    Value
from
    Argument a
left join
    ArgumentValue av
        on (a.ArgumentId = ava.ArgumentId) 
";

        public IQuery All()
        {
            return new RawQuery(string.Concat(SelectClause, "order by Name"));
        }

        public IQuery Get(Guid id)
        {
            return
                new RawQuery(string.Concat(SelectClause, "where ArgumentId = @ArgumentId")).AddParameterValue(
                    ArgumentColumns.Id, id);
        }

        public IQuery GetValues(Guid id)
        {
            return RawQuery.Create("select Value from ArgumentValue where ArgumentId = @ArgumentId")
                .AddParameterValue(ArgumentColumns.ValueColumns.ArgumentId, id);
        }

        public IQuery Add(Argument item)
        {
            return RawQuery.Create(@"
insert into Argument
(
    ArgumentId,
    Name,
    AnswerType
)
values
(
    @ArgumentId,
    @Name,
    @AnswerType
)")
                .AddParameterValue(ArgumentColumns.Id, item.Id)
                .AddParameterValue(ArgumentColumns.Name, item.Name)
                .AddParameterValue(ArgumentColumns.AnswerType, item.AnswerType);
        }

        public IQuery Remove(Guid id)
        {
            return
                RawQuery.Create("delete from Argument where ArgumentId = @ArgumentId")
                    .AddParameterValue(ArgumentColumns.Id, id);
        }


        public IQuery GetDTO(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClauseDTO, "where a.ArgumentId = @ArgumentId order by Name"))
                .AddParameterValue(ArgumentColumns.Id, id);
        }

        public IQuery Save(Argument item)
        {
            return RawQuery.Create(@"
update 
    Argument
set
    Name = @Name,
    AnswerType = @AnswerType
where
    ArgumentId = @ArgumentId
")
                .AddParameterValue(ArgumentColumns.Name, item.Name)
                .AddParameterValue(ArgumentColumns.AnswerType, item.AnswerType)
                .AddParameterValue(ArgumentColumns.Id, item.Id);
        }


        public IQuery RemoveValues(Argument argument)
        {
            return RawQuery.Create("delete from ArgumentValue where ArgumentId = @ArgumentId")
                .AddParameterValue(ArgumentColumns.ValueColumns.ArgumentId, argument.Id);
        }

        public IQuery SaveValue(Argument argument, string value)
        {
            throw new NotImplementedException();
        }

        public IQuery Get(string name)
        {
            return
                new RawQuery(string.Concat(SelectClause, "where ArgumentName = @ArgumentName")).AddParameterValue(
                    ArgumentColumns.Name, name);
        }

        public IQuery Registered(PrimitiveEvent primitiveEvent, Registered registered)
        {
            return RawQuery.Create(@"
insert into Argument
(
    ArgumentId,
    Name,
    AnswerType
)
values
(
    @ArgumentId,
    @Name,
    @AnswerType
)")
                .AddParameterValue(ArgumentColumns.Id, primitiveEvent.Id)
                .AddParameterValue(ArgumentColumns.Name, registered.Name)
                .AddParameterValue(ArgumentColumns.AnswerType, registered.AnswerType);
        }

        public IQuery Removed(PrimitiveEvent primitiveEvent, Removed removed)
        {
            return
               RawQuery.Create("delete from Argument where ArgumentId = @ArgumentId")
                   .AddParameterValue(ArgumentColumns.Id, primitiveEvent.Id);
        }

        public IQuery Renamed(PrimitiveEvent primitiveEvent, Renamed renamed)
        {
            return RawQuery.Create(@"
update 
    Argument
set
    Name = @Name
where
    ArgumentId = @ArgumentId
")
               .AddParameterValue(ArgumentColumns.Name, renamed.Name)
               .AddParameterValue(ArgumentColumns.Id, primitiveEvent.Id);
        }

        public IQuery ValueAdded(PrimitiveEvent primitiveEvent, ValueAdded valueAdded)
        {
            return RawQuery.Create(@"
insert into ArgumentValue
(
    ArgumentId,
    Value
)
values
(
    @ArgumentId,
    @Value
)
")
                .AddParameterValue(ArgumentColumns.ValueColumns.ArgumentId, primitiveEvent.Id)
                .AddParameterValue(ArgumentColumns.ValueColumns.Value, valueAdded.Value);
        }

        public IQuery ValueRemoved(PrimitiveEvent primitiveEvent, ValueRemoved valueRemoved)
        {
            return RawQuery.Create(@"
delete 
from 
    ArgumentValue 
where 
    ArgumentId = @ArgumentId
and
    Value = @Value
")
                .AddParameterValue(ArgumentColumns.ValueColumns.ArgumentId, primitiveEvent.Id)
                .AddParameterValue(ArgumentColumns.ValueColumns.Value, valueRemoved.Value);
        }
    }
}