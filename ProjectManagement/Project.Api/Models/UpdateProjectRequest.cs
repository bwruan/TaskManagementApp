using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.Models
{
    public class UpdateProjectRequest
    {
        public long ProjectId { get; set; }

        public string NewName { get; set; }

        public string NewDescription { get; set; }

        public long NewOwnerId { get; set; }
    }
}
