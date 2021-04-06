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

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CompletedDate { get; set; }

        public DateTime DueDate { get; set; }
    }
}
