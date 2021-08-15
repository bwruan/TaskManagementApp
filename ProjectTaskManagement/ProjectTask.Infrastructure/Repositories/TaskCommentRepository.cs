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
            using(var context = new TaskManagementContext())
            {
                var comments = await context.TaskComments.Include(c => c.Task).Include(c => c.Commenter).Where(c => c.TaskId == taskId).ToListAsync();

                if(comments.Count == 0)
                {
                    return comments;
                }

                var commentList = new List<TaskComment>();

                foreach (var comment in comments)
                {
                    commentList.Add(comment);
                }

                var skipAmt = (page - 1) * 5;

                var totalPages = Math.Ceiling((commentList.Count / 5m));

                if(page > totalPages)
                {
                    throw new ArgumentException("No more comments on task.");
                }

                return commentList.Skip(skipAmt).Take(5).ToList();
            }
        }

        public async Task<decimal> GetLastPageOfCommentsList(long taskId)
        {
            using(var context = new TaskManagementContext())
            {
                var comments = await context.TaskComments.Include(c => c.Task).Include(c => c.Commenter).Where(c => c.TaskId == taskId).ToListAsync();

                var commentList = new List<TaskComment>();

                foreach (var comment in comments)
                {
                    commentList.Add(comment);
                }

                var lastPage = Math.Ceiling((commentList.Count / 5m));

                return lastPage;
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
