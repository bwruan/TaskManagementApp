using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.Models
{
    public class MemberRequest
    {
        public long ProjectId { get; set; }

        public string Email { get; set; }
    }
}
