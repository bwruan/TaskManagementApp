using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Infrastructure.Repositories
{
    public interface ITaskRepository
    {
        Task CreateTask(string name, string description, long projectId);

        Task UpdateTask(long taskId, string newName, string newDescription, DateTime newDueDate);

        Task<Entities.Task> GetTaskByTaskId(long taskId);

        Task<Entities.Task> GetTaskByName(string name);
    }
}
