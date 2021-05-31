using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTask.Api.Models
{
    public class UpdateCommentRequest
    {
        public long CommentId { get; set; }

        public string NewComment { get; set; }
    }
}
