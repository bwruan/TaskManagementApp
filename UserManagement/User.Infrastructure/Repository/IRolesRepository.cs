using System.Threading.Tasks;

namespace User.Infrastructure.Repository
{
    public interface IRolesRepository
    {
        Task AddRole(string name);
    }
}
