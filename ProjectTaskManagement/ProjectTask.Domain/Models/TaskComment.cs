using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTask.Domain.Models
{
    public class TaskComment
    {
        public long CommentId { get; set; }

        public string Comment { get; set; }

        public long TaskId { get; set; }

        public long CommenterId { get; set; }
    }
}
