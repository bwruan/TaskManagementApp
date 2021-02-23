using System;

namespace Project.Domain.Models
{
    public class Project
    {
        public long ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public long AccountId { get; set; }

        public Account OwnerAccount { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
