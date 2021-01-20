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

        //you ignored my comment about LINQ's include.
        //I told you that unless you do Include, the "Project" property of UserToProjects class is NULL.
        public async Task<List<Entities.Project>> GetProjectsByAccountId(long accountId)
        {
            using (var context = new TaskManagementContext())
            {
                var project = await context.UserToProjects.Where(p => p.Project.OwnerAccountId == accountId).ToListAsync(); 

                var projectList = new List<Entities.Project>();

                //Mappers do not belong in infrastructure. Where is your mapper in User Management service?
                //We dont do anything is repo asides retrieve things from DB.
                foreach (var dbProj in project) 
                {
                    projectList.Add(UserToProjectMapper.UserToProjectToProject(dbProj));
                }

                return projectList;
            }
        }

        //Is this even correct?
        //Think, how many accounts/workers can be assigned to a project? 
        //Also, you dont need to do the second statement! Think about how to do it with just one LIQN statement.
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
