using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models
{
    public class Account
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }
    }
}
