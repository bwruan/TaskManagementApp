using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTask.Api.Models
{
    public class CommentRequest
    {
        public long CommentId { get; set; }

        public string Comment { get; set; }

        public long TaskId { get; set; }

        public long CommenterId { get; set; }
    }
}
