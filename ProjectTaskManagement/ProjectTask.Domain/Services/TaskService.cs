using ProjectTask.Domain.Mapper;
using ProjectTask.Infrastructure.ProjectManagement;
using ProjectTask.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectService _projectService;

        public TaskService(ITaskRepository taskRepository, IProjectService projectService)
        {
            _taskRepository = taskRepository;
            _projectService = projectService;
        }

        public async Task CreateTask(string name, string description, long projectId, long taskeeId)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Task Name field blank");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Description field blank");
            }

            if (projectId <= 0)
            {
                throw new ArgumentException("Project Id error.");
            }

            if (taskeeId <= 0)
            {
                throw new ArgumentException("Account does not exist");
            }

            await _taskRepository.CreateTask(name, description, projectId, taskeeId);
        }

        public async Task<Models.Task> GetTaskByName(string name, string token)
        {
            var task = await _taskRepository.GetTaskByName(name);

            if (task == null)
            {
                throw new ArgumentException("Task does not exist.");
            }

            var coreTask = TaskMapper.DbTaskToCoreTask(task);
            var project = await _projectService.GetProjectById(coreTask.ProjectId, token);

            coreTask.CurrentProject = TaskMapper.CurrentProjectToCoreProject(project);

            return coreTask;
        }

        public async Task<Models.Task> GetTaskByTaskId(long taskId, string token)
        {
            var task = await _taskRepository.GetTaskByTaskId(taskId);

            if (task == null)
            {
                throw new ArgumentException("Task does not exist.");
            }

            var coreTask = TaskMapper.DbTaskToCoreTask(task);
            var project = await _projectService.GetProjectById(coreTask.ProjectId, token);

            coreTask.CurrentProject = TaskMapper.CurrentProjectToCoreProject(project);

            return coreTask;
        }

        public async Task MarkComplete(long taskId, bool isComplete)
        {
            var task = await _taskRepository.GetTaskByTaskId(taskId);

            if (task == null)
            {
                throw new ArgumentException("Task does not exist.");
            }
            
            if(task.IsCompleted == true)
            {
                throw new ArgumentException("Task already completed.");
            }

            await _taskRepository.MarkComplete(taskId, isComplete);
        }

        public async Task UpdateTask(long taskId, string newName, string newDescription, long newTaskeeId, DateTime newDueDate)
        {
            var task = await _taskRepository.GetTaskByTaskId(taskId);

            if (task == null)
            {
                throw new ArgumentException("Task does not exist.");
            }

            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentException("Task Name field blank");
            }

            if (string.IsNullOrEmpty(newDescription))
            {
                throw new ArgumentException("Task Description field blank");
            }

            if (newTaskeeId <= 0)
            {
                throw new ArgumentException("Account does not exist");
            }

            if (newDueDate == null)
            {
                throw new ArgumentException("Due Date empty.");
            }

            await _taskRepository.UpdateTask(taskId, newName, newDescription, newTaskeeId, newDueDate);
        }
    }
}
