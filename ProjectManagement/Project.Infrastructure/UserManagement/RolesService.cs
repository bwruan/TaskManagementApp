﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Project.Infrastructure.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.UserManagement
{
    public interface IRolesService
    {
        Task<List<Roles>> GetRoles();
    }

    public class RolesService : IRolesService
    {
        private readonly IConfiguration _configuration;
        private readonly string _url;

        public RolesService(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration.GetSection("Services:UserManagement").Value;
        }

        public async Task<List<Roles>> GetRoles()
        {
            var httpClient = new HttpClient();
      
            var response = await httpClient.GetAsync(_url + "/api/roles/roles");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Roles>>(result);
        }
    }
}
