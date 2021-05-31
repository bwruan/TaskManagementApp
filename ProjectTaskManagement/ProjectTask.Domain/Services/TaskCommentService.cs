using ProjectTask.Domain.Mapper;
using ProjectTask.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly ITaskCommentRepository _taskCommentRepository;

        public TaskCommentService(ITaskCommentRepository taskCommentRepository)
        {
            _taskCommentRepository = taskCommentRepository;
        }

        public async Task CreateComment(string comment, long commenterId)
        {
            if (string.IsNullOrEmpty(comment))
            {
                throw new ArgumentException("Please input comment.");
            }
            
            await _taskCommentRepository.CreateComment(comment, commenterId);
        }

        public async Task<Models.TaskComment> GetCommentByCommentId(long commentId, string token)
        {
            var comment = await _taskCommentRepository.GetCommentByCommentId(commentId);

            if(comment == null)
            {
                throw new ArgumentException("Comment does not exist");
            }

            var coreComment = TaskCommentMapper.DbCommentToCoreComment(comment);

            return coreComment;
        }

        public async Task UpdateComment(long commentId, string newComment)
        {
            var comment = await _taskCommentRepository.GetCommentByCommentId(commentId);

            if (comment == null)
            {
                throw new ArgumentException("Comment does not exist");
            }

            if (string.IsNullOrEmpty(newComment))
            {
                throw new ArgumentException("Please input comment");
            }

            await _taskCommentRepository.UpdateComment(commentId, newComment);
        }
    }
}
