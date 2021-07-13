using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Infrastructure.Repositories
{
    public interface ITaskRepository
    {
        Task<long> CreateTask(string name, string description, long projectId, long taskeeId, DateTime dueDate);

        Task UpdateTask(long taskId, string newName, string newDescription, long newTaskeeId, DateTime newDueDate);

        Task<DateTime> MarkComplete(long taskId, bool isComplete);

        Task<Entities.Task> GetTaskByTaskId(long taskId);

        Task<Entities.Task> GetTaskByName(string name);

        Task <List<Entities.Task>> GetTasksByProjectId(long projectId);

        Task RemoveTask(long taskId);
    }
}
