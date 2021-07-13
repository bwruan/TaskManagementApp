using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectTask.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProjectTask.Infrastructure.ProjectManagement
{
    public interface IUserToProjectService
    {
        Task<List<Account>> GetAccountByProjectId(long projectId, string token);
    }

    public class UserToProjectService : IUserToProjectService
    {
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public UserToProjectService(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration.GetSection("Services:ProjectManagement").Value;
        }

        public async Task<List<Account>> GetAccountByProjectId(long projectId, string token)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(_url + "api/usertoproject/" + projectId);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Account>>(result);
        }
    }
}
