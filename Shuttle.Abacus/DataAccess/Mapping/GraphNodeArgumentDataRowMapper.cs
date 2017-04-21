using System.Data;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess.Mapping
{
    public class GraphNodeArgumentDataRowMapper : IDataRowMapper<GraphNodeArgument>
    {
        private readonly IORM orm;

        public GraphNodeArgumentDataRowMapper(IORM orm)
        {
            this.orm = orm;
        }

        public GraphNodeArgument MapFrom(DataRow input)
        {
            return
                new GraphNodeArgument(
                    orm.MapDataReader<Argument>(ArgumentTableAccess.Get(GraphNodeArgumentColumns.ArgumentId.MapFrom(input))).First(),
                    GraphNodeArgumentColumns.Format.MapFrom(input));
        }
    }
}
