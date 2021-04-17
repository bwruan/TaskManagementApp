using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ProjectTask.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public async Task CreateTask(string name, string description, long projectId, long taskeeId)
        {
            using (var context = new Entities.TaskManagementContext())
            {
                context.Tasks.Add(new Entities.Task()
                {
                    TaskName = name,
                    TaskDescription = description,
                    ProjectId = projectId,
                    TaskeeId = taskeeId                    
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<Entities.Task> GetTaskByName(string name)
        {
            using(var context = new Entities.TaskManagementContext())
            {
                return await context.Tasks.FirstOrDefaultAsync(t => t.TaskName == name);
            }
        }

        public async Task<Entities.Task> GetTaskByTaskId(long taskId)
        {
            using (var context = new Entities.TaskManagementContext())
            {
                return await context.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);
            }
        }

        public async Task MarkComplete(long taskId, bool isComplete)
        {
            using (var context = new Entities.TaskManagementContext())
            {
                var task = await context.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);

                task.IsCompleted = isComplete;
                task.CompletedDate = DateTime.Now;

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateTask(long taskId, string newName, string newDescription, long newTaskeeId, DateTime newDueDate)
        {
            using (var context = new Entities.TaskManagementContext())
            {
                var task = await context.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);

                task.TaskName = newName;
                task.TaskDescription = newDescription;
                task.DueDate = newDueDate;
                task.TaskeeId = newTaskeeId;
                task.UpdatedDate = DateTime.Now;

                await context.SaveChangesAsync();
            }
        }
    }
}
