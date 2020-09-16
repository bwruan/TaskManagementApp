using System.Threading.Tasks;
using User.Infrastructure.Repository.Entities;

namespace User.Infrastructure.Repository
{
    public interface IAccountRepository
    {
        Task CreateAccount(string name, string email, string password, int roleId, byte[] image);

        Task<Account> GetAccountByName(string name);

        Task UpdatePassword(string name, string newPassword);

        Task UpdateRole(string name, int newRole);
    }
}
