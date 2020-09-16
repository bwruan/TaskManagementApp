using System;
using System.Collections.Generic;

namespace User.Infrastructure.Repository.Entities
{
    public partial class Account
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public byte[] ProfilePic { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? Active { get; set; }

        public virtual Roles Role { get; set; }
    }
}
