﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public interface ITaskService
    {
        Task CreateTask(string name, string description, long projectId, long taskeeId);

        Task UpdateTask(long taskId, string newName, string newDescription, long newTaskeeId, bool isComplete, DateTime newDueDate);

        Task<Models.Task> GetTaskByTaskId(long taskId, string token);

        Task<Models.Task> GetTaskByName(string name, string token);
    }
}
