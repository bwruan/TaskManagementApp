using Project.Infrastructure.UserManagement.Models;
using System;

namespace Project.Infrastructure.ProjectTaskManagement.Models
{
    public class Task
    {
        public long TaskId { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public long ProjectId { get; set; }

        public bool IsCompleted { get; set; }

        public long TaskeeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime DueDate { get; set; }
    }
}
