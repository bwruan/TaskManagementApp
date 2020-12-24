using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using User.Domain.Models;
using User.Domain.Services;
using User.Infrastructure.Repository;
using EfAccount = User.Infrastructure.Repository.Entities.Account;

namespace User.Test.Service
{
    [TestFixture]
    public class AccountServiceTest
    {
        private Mock<IAccountRepository> _accountRepository;

        [SetUp]
        public void Setup()
        {
            _accountRepository = new Mock<IAccountRepository>();
        }

        [Test]
        public void CreateAccount_NameEmpty()
        {
            _accountRepository.Setup(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.CreateAccount("", "email@email.com", "anyPassword", 1, null));
        }

        [Test]
        public void CreateAccount_NameNull()
        {
            _accountRepository.Setup(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.CreateAccount(null, "email@email.com", "anyPassword", 1, null));
        }

        [Test]
        public void CreateAccount_EmailEmpty()
        {
            _accountRepository.Setup(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.CreateAccount("name", "", "anyPassword", 1, null));
        }

        [Test]
        public void CreateAccount_EmailNull()
        {
            _accountRepository.Setup(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.CreateAccount("name", null, "anyPassword", 1, null));
        }

        [Test]
        public void CreateAccount_PasswordEmpty()
        {
            _accountRepository.Setup(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.CreateAccount("name", "email@email.com", "", 1, null));
        }

        [Test]
        public void CreateAccount_PasswordNull()
        {
            _accountRepository.Setup(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.CreateAccount("name", "email@email.com", null, 1, null));
        }

        [Test]
        public void CreateAccount_PasswordLessThanMinLength()
        {
            _accountRepository.Setup(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.CreateAccount("name", "email@email.com", "asd", 1, null));
        }

        [Test]
        public void CreateAccount_PasswordGreaterThanMaxLength()
        {
            _accountRepository.Setup(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.CreateAccount("name", "email@email.com", "veryveryextremelylongpassword1231231", 1, null));
        }

        [Test]
        public async Task CreateAccount_Success()
        {
            _accountRepository.Setup(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .Returns(Task.CompletedTask);

            var accountService = new AccountService(_accountRepository.Object);

            await accountService.CreateAccount("name", "email@email.com", "password123", 1, null);

            _accountRepository.Verify(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), 
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()), Times.Once);
        }

        [Test]
        public void GetAccountByEmail_AccountDoesNotExist()
        {
            _accountRepository.Setup(a => a.GetAccountByEmail(It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.GetAccount("email@email.com"));
        }

        [Test]
        public async Task GetAccountByEmail_Success()
        {
            _accountRepository.Setup(a => a.GetAccountByEmail(It.IsAny<string>()))
                .ReturnsAsync(new EfAccount());

            var accountService = new AccountService(_accountRepository.Object);

            await accountService.GetAccount("email@email.com");

            _accountRepository.Verify(a => a.GetAccountByEmail(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void LogIn_Fail()
        {
            _accountRepository.Setup(a => a.GetAccountByEmail(It.IsAny<string>()))
                .ReturnsAsync(new EfAccount() { Email = "email@email.com", Status = true });

            _accountRepository.Setup(a => a.UpdateStatus(It.IsAny<string>(), It.IsAny<string>(), true))
                .Returns(Task.CompletedTask);

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.LogIn("email@email.com", "password123"));
        }

        [Test]
        public async Task LogIn_Success()
        {
            _accountRepository.Setup(a => a.GetAccountByEmail(It.IsAny<string>()))
                .ReturnsAsync(new EfAccount() { Email = "email@email.com", Status = false });

            _accountRepository.Setup(a => a.UpdateStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.CompletedTask);

            var accountService = new AccountService(_accountRepository.Object);

            await accountService.LogIn("email@email.com", "password123");

            _accountRepository.Verify(a => a.UpdateStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void LogOut_Fail()
        {
            _accountRepository.Setup(a => a.GetAccountById(It.IsAny<long>()))
                .ReturnsAsync(new EfAccount() { Id = 1, Status = false });

            _accountRepository.Setup(a => a.UpdateStatus(It.IsAny<string>(), It.IsAny<string>(), false))
                .Returns(Task.CompletedTask);

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.LogOut(1));
        }

        [Test]
        public async Task LogOut_Success()
        {
            _accountRepository.Setup(a => a.GetAccountById(It.IsAny<long>()))
                .ReturnsAsync(new EfAccount() { Id = 1, Status = true });

            _accountRepository.Setup(a => a.UpdateStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.CompletedTask);

            var accountService = new AccountService(_accountRepository.Object);

            await accountService.LogOut(1);

            _accountRepository.Verify(a => a.UpdateStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void UpdateAccountInfo_NameEmpty()
        {
            _accountRepository.Setup(a => a.UpdateAccountInfo(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.UpdateAccountInfo(1,"", "newemail@email.com", 1, null));
        }

        [Test]
        public void UpdateAccountInfo_NameNull()
        {
            _accountRepository.Setup(a => a.UpdateAccountInfo(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.UpdateAccountInfo(1, null, "newemail@email.com", 1, null));
        }

        [Test]
        public void UpdateAccountInfo_EmailEmpty()
        {
            _accountRepository.Setup(a => a.UpdateAccountInfo(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.UpdateAccountInfo(1, "newName", "", 1, null));
        }

        [Test]
        public void UpdateAccountInfo_EmailNull()
        {
            _accountRepository.Setup(a => a.UpdateAccountInfo(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.UpdateAccountInfo(1, "newName", null, 1, null));
        }

        [Test]
        public async Task UpdateAccountInfo_Success()
        {
            _accountRepository.Setup(a => a.UpdateAccountInfo(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .Returns(Task.CompletedTask);

            var accountService = new AccountService(_accountRepository.Object);

            await accountService.UpdateAccountInfo(1, "newName", "newemail@email.com", 1, null);

            _accountRepository.Verify(a => a.UpdateAccountInfo(It.IsAny<long>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()), Times.Once);
        }

        [Test]
        public void UpdatePassword_PasswordEmpty()
        {
            _accountRepository.Setup(a => a.UpdatePassword(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.UpdatePassword(1, ""));
        }

        [Test]
        public void UpdatePassword_PasswordNull()
        {
            _accountRepository.Setup(a => a.UpdatePassword(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.UpdatePassword(1, null));
        }

        [Test]
        public void UpdatePassword_PasswordLessThanMinLength()
        {
            _accountRepository.Setup(a => a.UpdatePassword(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.UpdatePassword(1, "asd"));
        }

        [Test]
        public void UpdatePassword_PasswordGreaterThanMaxLength()
        {
            _accountRepository.Setup(a => a.UpdatePassword(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new ArgumentException());

            var accountService = new AccountService(_accountRepository.Object);

            Assert.ThrowsAsync<ArgumentException>(() => accountService.UpdatePassword(1, "veryveryextremelylongpassword1231231"));
        }

        [Test]
        public async Task UpdatePassword_Success()
        {
            _accountRepository.Setup(a => a.UpdatePassword(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var accountService = new AccountService(_accountRepository.Object);

            await accountService.UpdatePassword(1, "newPassword");

            _accountRepository.Verify(a => a.UpdatePassword(It.IsAny<long>(), It.IsAny<string>()), Times.Once);
        }
    }
}
