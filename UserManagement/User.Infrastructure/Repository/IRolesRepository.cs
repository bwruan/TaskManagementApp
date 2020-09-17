using System.Collections.Generic;
using System.Threading.Tasks;
using User.Infrastructure.Repository.Entities;

namespace User.Infrastructure.Repository
{
    public interface IRolesRepository
    {
        Task AddRole(string name);

        Task<List<Roles>> GetRoles();
    }
}
