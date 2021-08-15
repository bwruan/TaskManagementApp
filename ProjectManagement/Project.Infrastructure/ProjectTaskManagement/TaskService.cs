using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Project.Infrastructure.ProjectTaskManagement
{
    public interface ITaskService
    {
        Task RemoveTask(long taskId, string token);
        Task RemoveAllTaskFromProject(long projectId, string token);
    }

    public class TaskService : ITaskService
    {
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public TaskService(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration.GetSection("Services:ProjectTaskManagement").Value;
        }

        public async Task RemoveTask(long taskId, string token)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.DeleteAsync(_url + "/api/task/delete/" + taskId);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task RemoveAllTaskFromProject(long projectId, string token)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.DeleteAsync(_url + "/api/task/remove/all/" + projectId);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
