using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models
{
    public class UserToProject : Project
    {
        public long Id { get; set; }
        
        public long AccountId { get; set; }

        public string AccountName { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
