﻿using Project.Domain.Mapper;
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

            //its good but i think ur method names should reflect things
            //this method returns account ids so ur variable name should be accountIds
            //the repository method name should have been getAccountIdsByProjectIds
            var accounts = await _userToProjectRepository.GetAccountByProjectId(project.ProjectId);

            //as you loop through accountIds, u should do var id instead
            foreach(var acc in accounts)
            {
                var account = await _userService.GetAccountById(acc, token);
                accountList.Add(ProjectMapper.UserAccountToCoreAccount(account));
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
                var coreAccount = ProjectMapper.UserAccountToCoreAccount(account);
                
                //this is not what i mean by using it
                //think, you did it before in the other service.
                //u have a core account and a project. what did u do in the other service.
                if(coreAccount.Id != proj.OwnerAccountId)
                {
                    throw new ArgumentException("Account Id does not match Owner Id.");
                }

                projectList.Add(ProjectMapper.DbProjectToCoreProject(proj));
            }

            return projectList;
        }
    }
}
