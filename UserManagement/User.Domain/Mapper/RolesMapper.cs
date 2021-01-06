using DbRoles = User.Infrastructure.Repository.Entities.Roles;
using CoreRoles = User.Domain.Models.Roles;

namespace User.Domain.Mapper
{
    public static class RolesMapper
    {
        public static CoreRoles DbRolesToCoreRoles(DbRoles dbRoles)
        {
            var coreRoles = new CoreRoles();

            coreRoles.Id = dbRoles.Id;
            coreRoles.Name = dbRoles.Name;

            return coreRoles;
        }
    }
}
