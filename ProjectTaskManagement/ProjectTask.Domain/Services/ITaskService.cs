﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public interface ITaskService
    {
        Task<long> CreateTask(string name, string description, long projectId, long taskeeId, DateTime dueDate, string token);

        Task UpdateTask(long taskId, string newName, string newDescription, long newTaskeeId, DateTime newDueDate);

        Task<DateTime> MarkComplete(long taskId);

        Task<Models.Task> GetTaskByTaskId(long taskId, string token);

        Task<Models.Task> GetTaskByName(string name, string token);

        Task<List<Models.Task>> GetTasksByProjectId(long projectId, string token);

        Task RemoveTask(long taskId);

        Task RemoveAllTaskFromProject(long projectId);

    }
}
