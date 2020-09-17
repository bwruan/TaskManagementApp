using System.Collections.Generic;
using System.Threading.Tasks;
using User.Infrastructure.Repository.Entities;

namespace User.Infrastructure.Repository
{
    public class RolesRepository : IRolesRepository
    {
        public async Task AddRole(string name)
        {
            using(var context = new TaskManagementContext())
            {
                context.Roles.Add(new Roles() { Name = name });

                await context.SaveChangesAsync();
            }
        }

        public Task<List<Roles>> GetRoles()
        {
            throw new System.NotImplementedException();
        }
    }
}
