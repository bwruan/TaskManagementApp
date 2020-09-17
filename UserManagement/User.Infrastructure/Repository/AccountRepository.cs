using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Account> GetAccountByEmail(string email)
        {
            using (var context = new TaskManagementContext())
            {
                var account = await context.Account.FirstOrDefaultAsync(a => a.Email == email);

                if (account == null)
                {
                    throw new Exception("Account does not exist.");
                }

                return account;
            }
        }

        public async Task<Account> GetAccountByName(string name)
        {
            using (var context = new TaskManagementContext())
            {
                var account = await context.Account.FirstOrDefaultAsync(a => a.Name == name);

                if(account == null)
                {
                    throw new Exception("Account does not exist.");
                }

                return account;
            }
        }

        public async Task UpdatePassword(string name, string newPassword)
        {
            using (var context = new TaskManagementContext())
            {
                var account = await context.Account.FirstOrDefaultAsync(a => a.Name == name);

                if (account == null)
                {
                    throw new Exception("Account does not exist.");
                }

                account.Password = newPassword;

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateRole(string name, int newRoleId)
        {
            using (var context = new TaskManagementContext())
            {
                var account = await context.Account.FirstOrDefaultAsync(a => a.Name == name);

                if (account == null)
                {
                    throw new Exception("Account does not exist.");
                }

                account.RoleId = newRoleId;

                await context.SaveChangesAsync();
            }
        }
    }
}
