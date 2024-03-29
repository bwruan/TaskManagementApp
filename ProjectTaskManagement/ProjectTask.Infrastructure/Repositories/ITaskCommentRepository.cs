﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Infrastructure.Repositories
{
    public interface ITaskCommentRepository
    {
        Task CreateComment(string comment, long commenterId, long taskId);

        Task UpdateComment(long commentId, string newComment);

        Task<Entities.TaskComment> GetCommentByCommentId(long commentId);

        Task<List<Entities.TaskComment>> GetCommentsByTaskId(long taskId, int page);

        Task<decimal> GetLastPageOfCommentsList(long taskId);
    }
}
