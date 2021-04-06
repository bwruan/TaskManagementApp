using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTask.Domain.Models
{
    public class Project
    {
        public long ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public long AccountId { get; set; }
    }
}
