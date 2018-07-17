using System;
using Shuttle.Abacus.Events.Argument.v1;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public class ArgumentQueryFactory : IArgumentQueryFactory
    {
        private readonly string SelectClause = @"
select
    Id,
    Name,
    DataTypeName
from
    Argument
";

        public IQuery Search(ArgumentSearchSpecification specification)
        {
            Guard.AgainstNull(specification, nameof(specification));

            return new RawQuery(string.Concat(SelectClause, @"
where
(
    @Name is null
    or
    @Name = ''
    or
    Name like '%' + @Name + '%'
)
order by 
    Name
"))
                .AddParameterValue(Columns.Name, specification.Name);
        }

        public IQuery Get(Guid id)
        {
            return
                new RawQuery(string.Concat(SelectClause, "where Id = @Id")).AddParameterValue(
                    Columns.Id, id);
        }

        public IQuery Values(Guid id)
        {
            return RawQuery.Create("select Value from ArgumentValue where ArgumentId = @Id")
                .AddParameterValue(Columns.Id, id);
        }

        public IQuery Add(Argument item)
        {
            return RawQuery.Create(@"
insert into Argument
(
    Id,
    Name,
    DataTypeName
)
values
(
    @Id,
    @Name,
    @DataTypeName
)")
                .AddParameterValue(Columns.Id, item.Id)
                .AddParameterValue(Columns.Name, item.Name)
                .AddParameterValue(Columns.DataTypeName, item.DataType);
        }

        public IQuery Remove(Guid id)
        {
            return
                RawQuery.Create("delete from Argument where Id = @Id")
                    .AddParameterValue(Columns.Id, id);
        }

        public IQuery Save(Argument item)
        {
            return RawQuery.Create(@"
update 
    Argument
set
    Name = @Name,
    DataTypeName = @DataTypeName
where
    Id = @Id
")
                .AddParameterValue(Columns.Name, item.Name)
                .AddParameterValue(Columns.DataTypeName, item.DataType)
                .AddParameterValue(Columns.Id, item.Id);
        }


        public IQuery RemoveValues(Argument argument)
        {
            return RawQuery.Create("delete from ArgumentValue where ArgumentId = @Id")
                .AddParameterValue(Columns.Id, argument.Id);
        }

        public IQuery Get(string name)
        {
            return
                new RawQuery(string.Concat(SelectClause, "where ArgumentName = @ArgumentName")).AddParameterValue(
                    Columns.Name, name);
        }

        public IQuery Registered(PrimitiveEvent primitiveEvent, Registered registered)
        {
            return RawQuery.Create(@"
insert into Argument
(
    Id,
    Name,
    DataTypeName
)
values
(
    @Id,
    @Name,
    @DataTypeName
)")
                .AddParameterValue(Columns.Id, primitiveEvent.Id)
                .AddParameterValue(Columns.Name, registered.Name)
                .AddParameterValue(Columns.DataTypeName, registered.DataTypeName);
        }

        public IQuery Removed(PrimitiveEvent primitiveEvent, Removed removed)
        {
            return
                RawQuery.Create("delete from Argument where Id = @Id")
                    .AddParameterValue(Columns.Id, primitiveEvent.Id);
        }

        public IQuery Renamed(PrimitiveEvent primitiveEvent, Renamed renamed)
        {
            return RawQuery.Create(@"
update 
    Argument
set
    Name = @Name
where
    Id = @Id
")
                .AddParameterValue(Columns.Name, renamed.Name)
                .AddParameterValue(Columns.Id, primitiveEvent.Id);
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
    @Id,
    @Value
)
")
                .AddParameterValue(Columns.Id, primitiveEvent.Id)
                .AddParameterValue(Columns.Value, valueAdded.Value);
        }

        public IQuery ValueRemoved(PrimitiveEvent primitiveEvent, ValueRemoved valueRemoved)
        {
            return RawQuery.Create(@"
delete 
from 
    ArgumentValue 
where 
    ArgumentId = @Id
and
    Value = @Value
")
                .AddParameterValue(Columns.Id, primitiveEvent.Id)
                .AddParameterValue(Columns.Value, valueRemoved.Value);
        }
    }
}