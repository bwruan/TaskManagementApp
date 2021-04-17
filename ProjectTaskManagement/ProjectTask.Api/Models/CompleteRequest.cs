using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTask.Api.Models
{
    public class CompleteRequest
    {
        public long TaskId { get; set; }

        public bool IsComplete { get; set; }
    }
}
