using CoreComment = ProjectTask.Domain.Models.TaskComment;
using DbComment = ProjectTask.Infrastructure.Repositories.Entities.TaskComment;

namespace ProjectTask.Domain.Mapper
{
    public class TaskCommentMapper
    {
        public static CoreComment DbCommentToCoreComment(DbComment dbComment)
        {
            var coreComment = new CoreComment();

            coreComment.Comment = dbComment.Comment;
            coreComment.CommenterId = dbComment.CommenterId;
            coreComment.CommentId = dbComment.CommentId;
            coreComment.TaskId = dbComment.TaskId;

            return coreComment;
        }
    }
}
