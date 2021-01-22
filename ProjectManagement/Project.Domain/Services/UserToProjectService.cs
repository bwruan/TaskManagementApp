using Project.Domain.Mapper;
using Project.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Domain.Services
{
    public class UserToProjectService : IUserToProjectService
    {
        private readonly IUserToProjectRepository _userToProjectRepository;
        private readonly IProjectService _projectService;

        public UserToProjectService(IUserToProjectRepository userToProjectRepository, IProjectService projectService)
        {
            _userToProjectRepository = userToProjectRepository;
            _projectService = projectService;
        }

        public async Task<List<long>> GetAccountByProjectId(long projectId)
        {
            var project = await _projectService.GetProjectById(projectId);

            if(project == null)
            {
                throw new ArgumentException("Project does not exist.");
            }

            return await _userToProjectRepository.GetAccountByProjectId(project.ProjectId);
        }

        public async Task<List<Models.Project>> GetProjectsByAccountId(long accountId)
        {
            var projectList = new List<Models.Project>();

            var projects = await _userToProjectRepository.GetProjectsByAccountId(accountId);

            foreach(var proj in projects)
            {
                projectList.Add(ProjectMapper.DbProjectToCoreProject(proj));
            }

            return projectList;
        }
    }
}
