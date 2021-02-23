using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Infrastructure.Repository.Entities
{
    public partial class Project
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public long OwnerAccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
