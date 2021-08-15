using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.Models
{
    public class ProjectRequest
    {
        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public long OwnerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
