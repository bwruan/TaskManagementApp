using Project.Domain.Mapper;
using Project.Domain.Models;
using Project.Infrastructure.Repository;
using Project.Infrastructure.UserManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Domain.Services
{
    public class UserToProjectService : IUserToProjectService
    {
        private readonly IUserToProjectRepository _userToProjectRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserService _userService;

        public UserToProjectService(IUserToProjectRepository userToProjectRepository, IProjectRepository projectRepository, IUserService userService)
        {
            _userToProjectRepository = userToProjectRepository;
            _projectRepository = projectRepository;
            _userService = userService;
        }

        public async Task<List<Account>> GetAccountByProjectId(long projectId, string token)
        {
            var project = await _projectRepository.GetProjectById(projectId);

            if(project == null)
            {
                throw new ArgumentException("Project does not exist.");
            }

            var accountList = new List<Account>();

            var accountIds = await _userToProjectRepository.GetAccountIdsByProjectId(project.ProjectId);

            //change acc to id because you are looping through a list of accountIds, not accounts
            foreach(var acc in accountIds)
            {
                //this method returns an account, not ids. change this to acc
                var ids = await _userService.GetAccountById(acc, token);
                accountList.Add(ProjectMapper.UserAccountToCoreAccount(ids));
            }

            return accountList;
        }

        public async Task<List<Models.Project>> GetProjectsByAccountId(long accountId, string token)
        {
            var projectList = new List<Models.Project>();

            var projects = await _userToProjectRepository.GetProjectsByAccountId(accountId);

            foreach(var proj in projects)
            {
                var account = await _userService.GetAccountById(proj.OwnerAccountId, token);
                var corePorject = ProjectMapper.DbProjectToCoreProject(proj);

                corePorject.OwnerAccount = ProjectMapper.UserAccountToCoreAccount(account);

                projectList.Add(corePorject);
            }

            return projectList;
        }
    }
}
