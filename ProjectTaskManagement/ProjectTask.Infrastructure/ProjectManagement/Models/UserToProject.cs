using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTask.Infrastructure.ProjectManagement.Models
{
    public class UserToProject
    {
        public long ProjectId { get; set; }

        public long AccountId { get; set; }
    }
}
