using System;
using System.Threading.Tasks;
using User.Domain.Mapper;
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

            if(password.Length < 8 || password.Length > 32)
            {
                throw new ArgumentException("Password length not met.");
            }

            await _accountRepository.CreateAccount(name, email, password, roleId, image);
        }

        public async Task<Account> GetAccount(string email)
        {
            var account = await _accountRepository.GetAccountByEmail(email);

            return AccountMapper.DbAccountToCoreAccount(account);
        }

        public async Task LogIn(string email, string password)
        {
            var account = await _accountRepository.GetAccountByEmail(email);

            if(account.Status == true)
            {
                throw new ArgumentException("Account already online");
            }

            await _accountRepository.UpdateStatus(email, password, true);
        }

        public async Task LogOut(long id)
        {
            var account = await _accountRepository.GetAccountById(id);

            if (account.Status == false)
            {
                throw new ArgumentException("Account already offline");
            }

            await _accountRepository.UpdateStatus(account.Email, account.Password, false);
        }

        public async Task UpdateAccountInfo(long id, string newName, string newEmail, int newRoleId, byte[] newPic)
        {
            var account = await _accountRepository.GetAccountById(id);

            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentException("Name field blank.");
            }

            if (string.IsNullOrEmpty(newEmail))
            {
                throw new ArgumentException("Email field blank.");
            }

            await _accountRepository.UpdateAccountInfo(id, newName, newEmail, newRoleId, newPic);
        }

        public async Task UpdatePassword(long id, string newPassword)
        {
            var account = await _accountRepository.GetAccountById(id);

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException("Password field blank.");
            }

            if (newPassword.Length < 8 || newPassword.Length > 32)
            {
                throw new ArgumentException("Password length not met.");
            }

            await _accountRepository.UpdatePassword(id, newPassword);
        }
    }
}
