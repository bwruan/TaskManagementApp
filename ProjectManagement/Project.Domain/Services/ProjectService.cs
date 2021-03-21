﻿using Project.Domain.Mapper;
using Project.Infrastructure.Repository;
using Project.Infrastructure.UserManagement;
using System;
using System.Threading.Tasks;

namespace Project.Domain.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserService _userService;

        public ProjectService(IProjectRepository projectRepository, IUserService userService)
        {
            _projectRepository = projectRepository;
            _userService = userService;
        }

        public async Task CreateProject(string name, string description, long ownerId)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Project Name field blank");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Description field blank");
            }

            if(ownerId <= 0)
            {
                throw new ArgumentException("Owner Id error.");
            }
            
            await _projectRepository.CreateProject(name, description, ownerId);
        }

        public async Task<Models.Project> GetProjectById(long id, string token)
        {
            var project = await _projectRepository.GetProjectById(id);

            if(project == null)
            {
                throw new ArgumentException("Project does not exist");
            }

            var coreProject = ProjectMapper.DbProjectToCoreProject(project);
            var account = await _userService.GetAccountById(coreProject.AccountId, token);

            coreProject.OwnerAccount = ProjectMapper.UserAccountToCoreAccount(account);

            return coreProject;

        }

        public async Task<Models.Project> GetProjectByName(string name, string token)
        {
            var project = await _projectRepository.GetProjectByName(name);

            if(project == null)
            {
                throw new ArgumentException("Project does not exist.");
            }

            var coreProject = ProjectMapper.DbProjectToCoreProject(project);
            var account = await _userService.GetAccountById(coreProject.AccountId, token);

            coreProject.OwnerAccount = ProjectMapper.UserAccountToCoreAccount(account);

            return coreProject;
        }

        public async Task UpdateProject(long projectId, string newName, string newDescription, long newOwnerId)
        {
            var project = await _projectRepository.GetProjectById(projectId);

            if(project == null)
            {
                throw new ArgumentException("Project does not exist.");
            }

            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentException("Project Name field blank");
            }

            if (string.IsNullOrEmpty(newDescription))
            {
                throw new ArgumentException("Project Description field blank");
            }

            if (newOwnerId <= 0)
            {
                throw new ArgumentException("New Owner Id error.");
            }

            await _projectRepository.UpdateProject(projectId, newName, newDescription, newOwnerId);
        }
    }
}
