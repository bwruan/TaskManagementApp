using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectTask.Infrastructure.Repositories.Entities
{
    public partial class TaskComment
    {
        public long CommentId { get; set; }
        public string Comment { get; set; }
        public long TaskId { get; set; }
        public long CommenterId { get; set; }

        public virtual Task Task { get; set; }
    }
}
