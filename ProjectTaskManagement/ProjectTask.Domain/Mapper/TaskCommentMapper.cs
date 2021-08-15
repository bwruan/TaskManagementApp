using CoreComment = ProjectTask.Domain.Models.TaskComment;
using DbComment = ProjectTask.Infrastructure.Repositories.Entities.TaskComment;
using CoreAccount = ProjectTask.Domain.Models.Account;
using UserAccount = ProjectTask.Infrastructure.UserManagement.Models.Account;

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
            coreComment.Commenter = new CoreAccount() { Id = dbComment.Commenter.Id, Name = dbComment.Commenter.Name };

            return coreComment;
        }

        public static CoreAccount UserAccountToCoreAccount(UserAccount userAccount)
        {
            var coreAccount = new CoreAccount();

            coreAccount.Id = userAccount.Id;
            coreAccount.Name = userAccount.Name;
            coreAccount.Email = userAccount.Email;
            coreAccount.RoleId = userAccount.RoleId;
            coreAccount.RoleName = userAccount.RoleName;
            coreAccount.Status = userAccount.Status;

            return coreAccount;
        }
    }
}
