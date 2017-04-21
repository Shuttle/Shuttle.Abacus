namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class ValueSourceTypeQueries
    {
        
        
        public IQuery All()
        {
            return RawQuery.Create(@"
select
                Name,
                Text,
                Type,
                .From(TableName);
        }
    }
}
