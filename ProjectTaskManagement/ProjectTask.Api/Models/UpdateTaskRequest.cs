using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTask.Api.Models
{
    public class UpdateTaskRequest
    {
        public long TaskId { get; set; }

        public string NewName { get; set; } 

        public string NewDescription { get; set; }

        public long NewTaskeeId { get; set; }

        public DateTime NewDueDate { get; set; }
    }
}
