using System;

namespace Project.Domain.Models
{
    public class Project
    {
        public long ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public long OwnerAccountId { get; set; }

        //change this name to say OwnerAccount
        //OwnerAccount has Id as property so u dont need OwnerAccountId above (redundant)
        public Account Account { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
