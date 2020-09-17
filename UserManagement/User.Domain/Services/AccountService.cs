using System;
using System.Threading.Tasks;
using User.Domain.Models;
using User.Infrastructure.Repository;

namespace User.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository) 
        {
            _accountRepository = accountRepository;
        }

        public async Task CreateAccount(string name, string email, string password, int roleId, byte[] image)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name field blank.");
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email field blank.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password field blank.");
            }

            await _accountRepository.CreateAccount(name, email, password, roleId, image);
        }

        public async Task<Account> GetAccount(long id)
        {
            var account = await _accountRepository.GetAccountById(id);

            return account;
        }

        public Task LogIn(long id)
        {
            throw new NotImplementedException();
        }

        public Task LogOut(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAccountInfo(long id, string newName, string newEmail, int newRoleId, byte[] newPic)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePassword(long id, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
