using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using User.Infrastructure.Repository.Entities;

namespace User.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<long> CreateAccount(string name, string email, string password, int roleId, byte[] image)
        {
            using (var context = new TaskManagementContext())
            {
                var account = new Account()
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    RoleId = roleId,
                    ProfilePic = image
                };

                context.Account.Add(account);
                await context.SaveChangesAsync();

                return account.Id;
            }
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            using (var context = new TaskManagementContext())
            {
                var account = await context.Account.Include(a => a.Role).FirstOrDefaultAsync(a => a.Email == email);

                if (account == null)
                {
                    throw new ArgumentException("Account does not exist.");
                }

                return account;
            }
        }

        public async Task<Account> GetAccountById(long id)
        {
            using(var context = new TaskManagementContext())
            {
                var account = await context.Account.Include(a => a.Role).FirstOrDefaultAsync(a => a.Id == id);

                if (account == null)
                {
                    throw new ArgumentException("Account does not exist.");
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
                    throw new ArgumentException("Account does not exist.");
                }

                return account;
            }
        }

        public async Task UpdatePassword(long id, string newPassword)
        {
            using (var context = new TaskManagementContext())
            {
                var account = await context.Account.FirstOrDefaultAsync(a => a.Id == id);

                if (account == null)
                {
                    throw new ArgumentException("Account does not exist.");
                }

                account.Password = newPassword;
                account.UpdatedDate = DateTime.Now;

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateStatus(string email, string password, bool status)
        {
            using (var context = new TaskManagementContext())
            {
                var account = await context.Account.FirstOrDefaultAsync(a => a.Email == email);

                if (account == null)
                {
                    throw new ArgumentException("Account does not exist.");
                }

                if(account.Password != password)
                {
                    throw new ArgumentException("Incorrect password.");
                }

                account.Status = status;
                account.UpdatedDate = DateTime.Now;

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAccountInfo(long id, string newName, string newEmail, int newRoleId, byte[] newPic)
        {
            using (var context = new TaskManagementContext())
            {
                var account = await context.Account.FirstOrDefaultAsync(a => a.Id == id);

                if (account == null)
                {
                    throw new ArgumentException("Account does not exist.");
                }

                account.Name = newName;
                account.Email = newEmail;
                account.RoleId = newRoleId;
                account.ProfilePic = newPic;
                account.UpdatedDate = DateTime.Now;

                await context.SaveChangesAsync();
            }
        }

        public async Task UploadProfilePic(byte[] proPic, long id)
        {
            using (var context = new TaskManagementContext())
            {
                var account = await context.Account.FirstOrDefaultAsync(a => a.Id == id);

                account.ProfilePic = proPic;
                account.UpdatedDate = DateTime.Now;

                await context.SaveChangesAsync();
            }
        }
    }
}
