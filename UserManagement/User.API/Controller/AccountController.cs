using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.API.Model;
using User.Domain.Services;

namespace User.API.Controller
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            try
            {
                await _accountService.CreateAccount(request.Name, request.Email, request.Password, request.RoleId, request.ProfilePic);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAccountById(long id)
        {
            try
            {
                var account = await _accountService.GetAccount(id);

                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPatch]
        [Route("login")]
        public async Task<IActionResult> LogIn([FromBody] BaseRequest request)
        {
            try
            {
                await _accountService.LogIn(request.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPatch]
        [Route("logout")]
        public async Task<IActionResult> LogOut([FromBody] BaseRequest request)
        {
            try
            {
                await _accountService.LogOut(request.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPatch]
        [Route("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordRequest request)
        {
            try
            {
                await _accountService.UpdatePassword(request.Id, request.NewPassword);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        [Route("userinfo")]
        public async Task<IActionResult> UpdateAccountInfo([FromBody] AccountInfoRequest request)
        {
            try
            {
                await _accountService.UpdateAccountInfo(request.Id, request.NewName, request.NewEmail, request.NewRoleId, request.NewPic);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}