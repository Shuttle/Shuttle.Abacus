using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class SystemDataRowMapper : IDataRowMapper<SystemUser>
    {
        public MappedRow<SystemUser> Map(DataRow row)
        {
            var id = SystemUserColumns.Id.MapFrom(row);

            var user = new SystemUser(id)
            {
                LoginName = SystemUserColumns.LoginName.MapFrom(row)
            };

            return new MappedRow<SystemUser>(row, user);
        }
    }
}