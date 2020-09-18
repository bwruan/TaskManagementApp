using System.Collections.Generic;
using System.Threading.Tasks;
using User.Domain.Models;

namespace User.Domain.Services
{
    public interface IRolesService
    {
        Task AddRole(string name);

        Task<List<Roles>> GetRoles();
    }
}
