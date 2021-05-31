using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectTask.Infrastructure.ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Infrastructure.ProjectManagement
{
    public interface IProjectService
    {
        Task CreateProject(Project newProject);
        Task<Project> GetProjectById(long id, string token);
        Task<Project> GetProjectByName(string name, string token);
    }

    public class ProjectService : IProjectService
    {
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public ProjectService(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration.GetSection("Services:ProjectManagement").Value;
        }

        public async Task CreateProject(Project newProject)
        {
            var httpClient = new HttpClient();

            var body = new StringContent(JsonConvert.SerializeObject(newProject), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(_url + "/api/project/create", body);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<Project> GetProjectById(long id, string token)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(_url + "/api/project/" + id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Project>(result);
        }

        public async Task<Project> GetProjectByName(string name, string token)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(_url + "/api/project?name=" + name);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Project>(result);
        }
    }
}
