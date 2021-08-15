using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTask.Api.Models
{
    public class TaskRequest
    {
        public long TaskId { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public long ProjectId { get; set; }

        public long TaskerId { get; set; }

        public long TaskeeId { get; set; }

        public DateTime DueDate { get; set; }
    }
}
