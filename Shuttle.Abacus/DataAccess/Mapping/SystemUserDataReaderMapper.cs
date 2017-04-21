using System.Collections.Generic;
using System.Data;
using Abacus.Domain;
using Abacus.Infrastructure;

namespace Abacus.Data
{
    public class SystemUserDataReaderMapper : IDataReaderMapper<SystemUser>
    {
        public IEnumerable<SystemUser> MapFrom(IDataReader input)
        {
            var result = new List<SystemUser>();

            while (input.Read())
            {
                var id = SystemUserColumns.Id.MapFrom(input);

                var user = result.Find(item => item.Id.Equals(id));

                if (user == null)
                {
                    user = new SystemUser(id)
                               {
                                   LoginName = SystemUserColumns.LoginName.MapFrom(input)
                               };

                    result.Add(user);
                }

                var identifier = PermissionColumns.Permission.MapFrom(input);

                if (!string.IsNullOrEmpty(identifier))
                {
                    user.Permissions.Add(new Permission(identifier));
                }
            }

            return result;
        }
    }
}
