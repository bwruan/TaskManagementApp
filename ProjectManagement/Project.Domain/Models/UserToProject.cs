using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models
{
    public class UserToProject
    {
        public long Id { get; set; }
        
        public long AccountId { get; set; }
        
        public long ProjectId { get; set; }
    }
}
