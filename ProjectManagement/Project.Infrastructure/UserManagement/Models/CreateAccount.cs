using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Infrastructure.UserManagement.Models
{
    public class CreateAccount
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public byte[] ProfilePic { get; set; }
    }
}
