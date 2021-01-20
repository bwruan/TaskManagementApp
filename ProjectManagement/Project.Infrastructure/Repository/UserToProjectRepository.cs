using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Repository.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public class UserToProjectRepository : IUserToProjectRepository
    {
        public async Task<List<Entities.Project>> GetProjectsByAccountId(long accountId)
        {
            using (var context = new TaskManagementContext())
            {
                var project = await context.UserToProjects.Include(p => p.Project).Where(p => p.AccountId == accountId).ToListAsync(); 

                var projectList = new List<Entities.Project>();

                foreach(var proj in project)
                {
                    projectList.Add(proj.Project);
                }

                return projectList;
            }
        }

        public async Task<List<long>> GetAccountByProjectId(long projectId)
        {
            using(var context = new TaskManagementContext())
            {
                var account = await context.UserToProjects.Include(p => p.Project).Where(p => p.ProjectId == projectId).Select(p => p.AccountId).ToListAsync();

                var accountList = new List<long>();

                foreach(var acc in account)
                {
                    accountList.Add(acc);
                }

                return accountList;
            }
        }
    }
}
