using System.Threading.Tasks;
using User.Domain.Models;

namespace User.Domain.Services
{
    public interface IAccountService
    {
        Task<long> CreateAccount(string name, string email, string password, int roleId, byte[] image);

        Task<long> LogIn(string email, string password);

        Task LogOut(long id);

        Task<Account> GetAccountByEmail(string email);

        Task<Account> GetAccountById(long id);

        Task UpdatePassword(long id, string newPassword);

        Task UpdateAccountInfo(long id, string newName, string newEmail, int newRoleId, byte[] newPic);

        Task UploadProfilePic(byte[] proPic, long id);
    }
}
