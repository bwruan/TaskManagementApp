using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTask.Domain.Models
{
    public class Task
    {
        public long TaskId { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public Project CurrentProject { get; set; }

        public long ProjectId { get; set; }

        public long TaskerId { get; set; }

        public long TaskeeId { get; set; }

        public bool IsCompleted { get; set; }

        public Account TaskerAccount { get; set; }

        public Account TaskeeAccount { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CompletedDate { get; set; }

        public DateTime DueDate { get; set; }
    }
}
