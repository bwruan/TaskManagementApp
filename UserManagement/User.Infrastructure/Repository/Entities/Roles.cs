using System;
using System.Collections.Generic;

namespace User.Infrastructure.Repository.Entities
{
    public partial class Roles
    {
        public Roles()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
