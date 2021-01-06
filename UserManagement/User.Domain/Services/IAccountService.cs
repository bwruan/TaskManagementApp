using System.Threading.Tasks;
using User.Domain.Models;

namespace User.Domain.Services
{
    public interface IAccountService
    {
        Task CreateAccount(string name, string email, string password, int roleId, byte[] image);

        Task LogIn(string email, string password);

        Task LogOut(long id);

        Task<Account> GetAccount(string email);

        Task UpdatePassword(long id, string newPassword);

        Task UpdateAccountInfo(long id, string newName, string newEmail, int newRoleId, byte[] newPic);
    }
}
