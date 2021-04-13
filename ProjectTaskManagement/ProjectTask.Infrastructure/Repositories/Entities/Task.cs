using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectTask.Infrastructure.Repositories.Entities
{
    public partial class Task
    {
        public Task()
        {
            TaskComments = new HashSet<TaskComment>();
            UserToTasks = new HashSet<UserToTask>();
        }

        public long TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public long ProjectId { get; set; }
        public long TaskerId { get; set; }
        public long TaskeeId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime DueDate { get; set; }

        public virtual ICollection<TaskComment> TaskComments { get; set; }
        public virtual ICollection<UserToTask> UserToTasks { get; set; }
    }
}
