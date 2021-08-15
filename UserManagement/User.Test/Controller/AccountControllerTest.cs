using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using User.API.Controller;
using User.API.Model;
using User.Domain.Models;
using User.Domain.Services;

namespace User.Test.Controller
{
    [TestFixture]
    public class AccountControllerTest
    {
        private Mock<IAccountService> _accountService;
        private Mock<IConfiguration> _configuration;

        [SetUp]
        public void Setup()
        {
            _accountService = new Mock<IAccountService>();
            _configuration = new Mock<IConfiguration>();

            _configuration.Setup(g => g.GetSection(It.Is<string>(s => s.Equals("Jwt:Key"))).Value)
                .Returns("ThisismySecretKey");
            _configuration.Setup(g => g.GetSection(It.Is<string>(s => s.Equals("Jwt:Issuer"))).Value)
                .Returns("test.com");
        }

        [Test]
        public async Task CreateAccount_Success()
        {
            _accountService.Setup(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ReturnsAsync(1);

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.CreateAccount(new AccountRequest()
            {
                Name = "name",
                Email = "email@email.com",
                Password = "password123",
                RoleId = 1
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task CreateAccount_InternalServerError()
        {
            _accountService.Setup(a => a.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new Exception());

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.CreateAccount(new AccountRequest()
            {
                Name = "name",
                Email = "email@email.com",
                Password = "password123",
                RoleId = 1
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        //[Test]
        //public async Task UploadProfilePic_Success()
        //{
        //    _accountService.Setup(a => a.UploadProfilePic(It.IsAny<byte[]>(), It.IsAny<long>()))
        //        .Returns(Task.CompletedTask);

        //    var controller = new AccountController(_accountService.Object, _configuration.Object);

        //    var response = await controller.UploadProfilePic(1);

        //    Assert.NotNull(response);
        //    Assert.AreEqual(response.GetType(), typeof(OkResult));

        //    var ok = (OkResult)response;

        //    Assert.AreEqual(ok.StatusCode, 200);
        //}

        [Test]
        public async Task UploadProfilePic_InternalServerError()
        {
            _accountService.Setup(a => a.UploadProfilePic(It.IsAny<byte[]>(), It.IsAny<long>()))
                .ThrowsAsync(new Exception());

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.UploadProfilePic(1);

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task GetAccountByEmail_Success()
        {
            _accountService.Setup(a => a.GetAccountByEmail(It.IsAny<string>()))
                .ReturnsAsync(new Account());

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.GetAccountByEmail("email@email.com");

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var okObj = (OkObjectResult)response;

            Assert.AreEqual(okObj.StatusCode, 200);
        }

        [Test]
        public async Task GetAccountByEmail_InternalServerError()
        {
            _accountService.Setup(a => a.GetAccountByEmail(It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.GetAccountByEmail("email@email.com");

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task LogIn_Success()
        {
            _accountService.Setup(a => a.LogIn(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(1);

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.LogIn(new LoginRequest() 
            { 
                Email = "email@email.com",
                Password = "password123"
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));

            var ok = (OkObjectResult)response;

            Assert.AreEqual(ok.StatusCode, 200);
        }

        [Test]
        public async Task LogIn_InternalServerError()
        {
            _accountService.Setup(a => a.LogIn(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.LogIn(new LoginRequest()
            {
                Email = "email@email.com",
                Password= "password123"
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task LogOut_Success()
        {
            _accountService.Setup(a => a.LogOut(It.IsAny<long>()))
                .Returns(Task.CompletedTask);

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.LogOut(new BaseRequest()
            {
                Id = 1
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkResult));

            var ok = (OkResult)response;

            Assert.AreEqual(ok.StatusCode, 200);
        }

        [Test]
        public async Task LogOut_InternalServerError()
        {
            _accountService.Setup(a => a.LogOut(It.IsAny<long>()))
                .ThrowsAsync(new Exception());

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.LogOut(new BaseRequest()
            {
                Id = 1
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task UpdatePassword_Success()
        {
            _accountService.Setup(a => a.UpdatePassword(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.UpdatePassword(new PasswordRequest()
            {
                Id = 1,
                NewPassword = "newPassword"
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkResult));

            var ok = (OkResult)response;

            Assert.AreEqual(ok.StatusCode, 200);
        }

        [Test]
        public async Task UpdatePassword_InternalServerError()
        {
            _accountService.Setup(a => a.UpdatePassword(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.UpdatePassword(new PasswordRequest()
            {
                Id = 1,
                NewPassword = "newPassword"
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }

        [Test]
        public async Task UpdateAccountInfo_Success()
        {
            _accountService.Setup(a => a.UpdateAccountInfo(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .Returns(Task.CompletedTask);

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.UpdateAccountInfo(new AccountInfoRequest()
            {
                Id = 1,
                NewName = "newName",
                NewEmail = "newemail@email.com",
                NewRoleId = 2
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(OkResult));

            var ok = (OkResult)response;

            Assert.AreEqual(ok.StatusCode, 200);
        }

        [Test]
        public async Task UpdateAccountInfo_InternalServerError()
        {
            _accountService.Setup(a => a.UpdateAccountInfo(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte[]>()))
                .ThrowsAsync(new Exception());

            var controller = new AccountController(_accountService.Object, _configuration.Object);

            var response = await controller.UpdateAccountInfo(new AccountInfoRequest()
            {
                Id = 1,
                NewName = "newName",
                NewEmail = "newemail@email.com",
                NewRoleId = 2
            });

            Assert.NotNull(response);
            Assert.AreEqual(response.GetType(), typeof(ObjectResult));

            var obj = (ObjectResult)response;

            Assert.AreEqual(obj.StatusCode, 500);
        }
    }
}
