using ProjectTask.Domain.Mapper;
using ProjectTask.Infrastructure.ProjectManagement;
using ProjectTask.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task CreateTask(string name, string description, long projectId)
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

            await _taskRepository.CreateTask(name, description, projectId);
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

        public Task<Models.Task> GetTaskByTaskId(long taskId, string token)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTask(long taskId, string newName, string newDescription, DateTime newDueDate)
        {
            throw new NotImplementedException();
        }
    }
}
