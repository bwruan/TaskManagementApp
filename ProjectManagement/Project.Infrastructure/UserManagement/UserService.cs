using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Project.Infrastructure.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.UserManagement
{
    public interface IUserService
    {
        Task CreateAccount(CreateAccount newAccount);
        Task<Account> GetAccountByEmail(string email, string token);
        Task<Account> GetAccountById(long id, string token);
        Task<LoginResponse> LogIn(string email, string password);
    }

    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration.GetSection("Services:UserManagement").Value;
        }

        public async Task<Account> GetAccountByEmail(string email, string token)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(_url + "/api/account?email=" + email);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Account>(result);
        }

        public async Task<Account> GetAccountById(long id, string token)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(_url + "/api/account/" + id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Account>(result);
        }

        public async Task CreateAccount(CreateAccount newAccount)
        {
            var httpClient = new HttpClient();

            var body = new StringContent(JsonConvert.SerializeObject(newAccount), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(_url + "/api/account/create", body);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<LoginResponse> LogIn(string email, string password)
        {
            var httpClient = new HttpClient();

            var logIn = new LogIn()
            {
                Email = email,
                Password = password
            };

            var body = new StringContent(JsonConvert.SerializeObject(logIn), Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync(_url + "/api/account/login", body);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<LoginResponse>(result);
        }
    }
}
