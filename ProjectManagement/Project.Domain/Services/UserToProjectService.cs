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

        //this methods returns Account but you are returning ID. Returning an Id is useless to UI perspective.
        //Thiunk about what UI/Users want to see, do they jsut want to see Id or do they want to actually see the names, emails of the accounts tied to project.
        //Right?
        //I think you should: 
        //1. create a core account model.
        //2. call your repository to check if project exists
        //3. call repository to get all account ids tied to project.
        //4. loop through each account id returned from step 3. and call user service to get user account by the id.
        //5. map each account returned by user servce to ur core account model. Of, if the account has a role, get role with ur role service and map it too.
        //6. add to list of core account models.
        //7. return list of accounts
        public async Task<List<Account>> GetAccountByProjectId(long projectId)
        {
            var project = await _projectRepository.GetProjectById(projectId);

            if(project == null)
            {
                throw new ArgumentException("Project does not exist.");
            }

            var coreProject = ProjectMapper.DbProjectToCoreProject(project);
            var accountList = new List<Account>();

            var accounts = await _userToProjectRepository.GetAccountByProjectId(coreProject.ProjectId);

            foreach(var acc in accounts)
            {
               
            }

            return accountList;
        }

        public async Task<List<Models.Project>> GetProjectsByAccountId(long accountId)
        {
            var projectList = new List<Models.Project>();

            var projects = await _userToProjectRepository.GetProjectsByAccountId(accountId);

            foreach(var proj in projects)
            {
                //again, call user service to get account owner's information and map it to the model before adding to list.
                projectList.Add(ProjectMapper.DbProjectToCoreProject(proj));
            }

            return projectList;
        }
    }
}
