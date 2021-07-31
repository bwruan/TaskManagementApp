using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public interface ITaskCommentService
    {
        Task CreateComment(string comment, long commenterId, long taskId);

        Task UpdateComment(long commentId, string newComment);

        Task<Models.TaskComment> GetCommentByCommentId(long commentId, string token);
    }
}
