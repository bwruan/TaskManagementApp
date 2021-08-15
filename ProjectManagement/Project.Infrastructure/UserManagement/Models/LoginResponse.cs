using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Infrastructure.UserManagement.Models
{
    public class LoginResponse
    {
        [JsonProperty("accountId")]
        public long AccountId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
