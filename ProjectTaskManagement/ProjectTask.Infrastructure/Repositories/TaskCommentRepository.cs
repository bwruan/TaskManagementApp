using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Infrastructure.Repositories
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        public async Task CreateComment(string comment, long commenterId)
        {
            using (var context = new Entities.TaskManagementContext())
            {
                context.TaskComments.Add(new Entities.TaskComment()
                {
                    Comment = comment,
                    CommenterId = commenterId
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<Entities.TaskComment> GetCommentByCommentId(long commentId)
        {
            using(var context = new Entities.TaskManagementContext())
            {
                return await context.TaskComments.FirstOrDefaultAsync(c => c.CommentId == commentId);
            }
        }

        public async Task UpdateComment(long commentId, string newComment)
        {
            using(var context = new Entities.TaskManagementContext())
            {
                var comment = await context.TaskComments.FirstOrDefaultAsync(c => c.CommentId == commentId);

                comment.Comment = newComment;

                await context.SaveChangesAsync();
            }
        }
    }
}
