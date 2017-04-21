namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class ConstraintTypeQueries
    {
        

        public IQuery All()
        {
            return RawQuery.Create(@"
select
                Name,
                Text,
                EnabledForRestrictedAnswers,
                .From(TableName);
        }
    }
}
