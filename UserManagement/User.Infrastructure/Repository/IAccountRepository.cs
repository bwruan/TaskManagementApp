using System.Threading.Tasks;

namespace User.Infrastructure.Repository
{
    public interface IAccountRepository
    {
        Task CreateAccount(string name, string email, string password, int roleId, byte[] image);
    }
}
