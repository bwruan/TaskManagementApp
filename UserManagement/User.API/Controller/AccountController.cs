using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using User.API.Model;
using User.Domain.Services;

namespace User.API.Controller
{
    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            try
            {
                await _accountService.CreateAccount(request.Name, request.Email, request.Password, request.RoleId, request.ProfilePic);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAccountByEmail(string email)
        {
            try
            {
                var account = await _accountService.GetAccount(email);

                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody] AccountRequest request)
        {
            try
            {
                await _accountService.LogIn(request.Email, request.Password);

                var token = GenerateJSONWebToken();

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
                return StatusCode(500, ex.Message);
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
                return StatusCode(500, ex.Message);
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
                return StatusCode(500, ex.Message);
            }
        }

        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration.GetSection("Jwt:Issuer").Value,
              _configuration.GetSection("Jwt:Issuer").Value,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}