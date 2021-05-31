using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectTask.Infrastructure.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Infrastructure.UserManagement
{
    public interface IUserService
    {
        Task<Account> GetAccountById(long id, string token);
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
    }
}
