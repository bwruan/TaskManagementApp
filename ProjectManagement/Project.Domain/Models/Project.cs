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

        //should probably add owner Name, email, and role as property here.
        //Even better than adding individual properties, u'll be creating an Account Model as you'll see in later comments
        //you can create an Account Property here.

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
