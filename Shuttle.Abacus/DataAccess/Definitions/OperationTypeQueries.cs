namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class OperationTypeQueries
    {
        

        public IQuery All()
        {
            return RawQuery.Create(@"
select
                Name,
                Text,
                .From(TableName);
        }
    }
}
