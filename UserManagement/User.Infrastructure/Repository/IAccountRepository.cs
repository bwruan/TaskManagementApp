using System.Threading.Tasks;
using User.Infrastructure.Repository.Entities;

namespace User.Infrastructure.Repository
{
    public interface IAccountRepository
    {
        Task CreateAccount(string name, string email, string password, int roleId, byte[] image);

        Task<Account> GetAccountByName(string name);

        Task<Account> GetAccountByEmail(string email);

        Task<Account> GetAccountById(long id);

        Task UpdatePassword(long id, string newPassword);

        Task UpdateAccountInfo(long id, string newName, string newEmail, int newRoleId, byte[]newPic);

        Task UpdateStatus(string email, string password, bool status);
    }
}
