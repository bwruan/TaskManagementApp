using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectTask.Infrastructure.Repositories.Entities
{
    public partial class Account
    {
        public Account()
        {
            TaskComments = new HashSet<TaskComment>();
            Tasks = new HashSet<Task>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public byte[] ProfilePic { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<TaskComment> TaskComments { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
