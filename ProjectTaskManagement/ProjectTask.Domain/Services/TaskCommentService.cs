using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public class TaskCommentService : ITaskCommentService
    {
        public Task CreateComment(string comment, long commenterId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.TaskComment> GetCommentByCommentId(long commentId, string token)
        {
            throw new NotImplementedException();
        }

        public Task UpdateComment(long commentId, string newComment)
        {
            throw new NotImplementedException();
        }
    }
}
