using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTask.Domain.Models
{
    public class UserToTask
    {
        public long Id { get; set; }

        public long AccountId { get; set; }

        public long TaskId { get; set; }
    }
}
