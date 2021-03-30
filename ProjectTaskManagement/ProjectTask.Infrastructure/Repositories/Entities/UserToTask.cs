using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectTask.Infrastructure.Repositories.Entities
{
    public partial class UserToTask
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public long TaskId { get; set; }

        public virtual Task Task { get; set; }
    }
}
