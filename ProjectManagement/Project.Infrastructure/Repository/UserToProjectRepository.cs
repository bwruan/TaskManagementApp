using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Mapper;
using Project.Infrastructure.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public class UserToProjectRepository : IUserToProjectRepository
    {
        public async Task<List<Entities.Project>> GetProjectsByAccountId(long accountId)
        {
            using (var context = new TaskManagementContext())
            {
                var project = await context.UserToProjects.Where(p => p.Project.OwnerAccountId == accountId).ToListAsync(); 

                var projectList = new List<Entities.Project>();

                foreach (var dbProj in project) 
                {
                    projectList.Add(UserToProjectMapper.UserToProjectToProject(dbProj));
                }

                return projectList;
            }
        }

        public async Task<long> GetAccountByProjectId(long projectId)
        {
            using(var context = new TaskManagementContext())
            {
                var project = await context.UserToProjects.FirstOrDefaultAsync(p => p.Project.ProjectId == projectId);

                var account = await context.UserToProjects.FirstOrDefaultAsync(p => p.AccountId == project.AccountId);

                return account.AccountId;
            }
        }
    }
}
