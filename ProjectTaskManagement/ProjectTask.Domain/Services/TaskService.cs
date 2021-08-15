using ProjectTask.Domain.Mapper;
using ProjectTask.Infrastructure.ProjectManagement;
using ProjectTask.Infrastructure.Repositories;
using ProjectTask.Infrastructure.UserManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public TaskService(ITaskRepository taskRepository, IProjectService projectService, IUserService userService)
        {
            _taskRepository = taskRepository;
            _projectService = projectService;
            _userService = userService;
        }

        public async Task<long> CreateTask(string name, string description, long projectId, long taskeeId, DateTime dueDate, string token)
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

            var taskeeAccount = await _userService.GetAccountById(taskeeId, token);

            if (taskeeAccount == null)
            {
                throw new ArgumentException("Account does not exist");
            }

            var taskId = await _taskRepository.CreateTask(name, description, projectId, taskeeAccount.Id, dueDate);

            return taskId;
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

        public async Task<List<Models.Task>> GetTasksByProjectId(long projectId, string token)
        {
            var taskList = new List<Models.Task>();

            var tasks = await _taskRepository.GetTasksByProjectId(projectId);

            foreach (var task in tasks)
            {
                var project = await _projectService.GetProjectById(task.ProjectId, token);
                var coreTask = TaskMapper.DbTaskToCoreTask(task);

                coreTask.CurrentProject = TaskMapper.CurrentProjectToCoreProject(project);

                taskList.Add(coreTask);
            }

            return taskList;
        }

        public async Task<DateTime> MarkComplete(long taskId)
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

            return await _taskRepository.MarkComplete(task.TaskId, true);
        }

        public async Task RemoveAllTaskFromProject(long projectId)
        {
            await _taskRepository.RemoveAllTaskFromProject(projectId);
        }

        public async Task RemoveTask(long taskId)
        {
            var task = await _taskRepository.GetTaskByTaskId(taskId);

            if (task == null)
            {
                throw new ArgumentException("Task does not exist.");
            }

            await _taskRepository.RemoveTask(task.TaskId);
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
