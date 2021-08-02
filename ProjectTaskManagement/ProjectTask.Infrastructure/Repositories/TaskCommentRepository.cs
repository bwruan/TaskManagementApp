using Microsoft.EntityFrameworkCore;
using ProjectTask.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Infrastructure.Repositories
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        public async System.Threading.Tasks.Task CreateComment(string comment, long commenterId, long taskId)
        {
            using (var context = new Entities.TaskManagementContext())
            {
                context.TaskComments.Add(new Entities.TaskComment()
                {
                    Comment = comment,
                    CommenterId = commenterId,
                    TaskId = taskId
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<TaskComment> GetCommentByCommentId(long commentId)
        {
            using(var context = new Entities.TaskManagementContext())
            {
                return await context.TaskComments.FirstOrDefaultAsync(c => c.CommentId == commentId);
            }
        }

        public async Task<List<TaskComment>> GetCommentsByTaskId(long taskId, int page)
        {
            using(var context = new Entities.TaskManagementContext())
            {
                var comments = await context.TaskComments.Include(c => c.Task).Where(c => c.TaskId == taskId).ToListAsync();

                var commentList = new List<Entities.TaskComment>();

                foreach (var comment in comments)
                {
                    commentList.Add(comment);
                }

                var skipAmt = (page - 1) * 5;

                if(skipAmt > commentList.Count)
                {
                    throw new ArgumentException("No more comments on task.");
                }

                return commentList.Skip(skipAmt).Take(5).ToList();
            }
        }

        public async System.Threading.Tasks.Task UpdateComment(long commentId, string newComment)
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
