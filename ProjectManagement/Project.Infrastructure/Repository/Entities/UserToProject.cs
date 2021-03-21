using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Infrastructure.Repository.Entities
{
    public partial class UserToProject
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public long ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}
