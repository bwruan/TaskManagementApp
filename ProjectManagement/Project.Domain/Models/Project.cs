using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models
{
    public class Project
    {
        public long ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public long OwnerAccountId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
