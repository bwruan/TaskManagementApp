using System.Threading.Tasks;
using User.Infrastructure.Repository.Entities;

namespace User.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public async Task CreateAccount(string name, string email, string password, int roleId, byte[] image)
        {
            using (var context = new TaskManagementContext())
            {
                context.Account.Add(new Account()
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    RoleId = roleId,
                    ProfilePic = image
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
