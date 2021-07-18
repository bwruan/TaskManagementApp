using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.Models
{
    public class RemoveMemberRequest
    {
        public long ProjectId { get; set; }

        public long AccountId { get; set; }
    }
}
