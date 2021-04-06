using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public interface ITaskService
    {
        Task CreateTask(string name, string description, long projectId);

        Task UpdateTask(long taskId, string newName, string newDescription, DateTime newDueDate);

        Task<Models.Task> GetTaskByTaskId(long taskId, string token);

        Task<Models.Task> GetTaskByName(string name, string token);
    }
}
