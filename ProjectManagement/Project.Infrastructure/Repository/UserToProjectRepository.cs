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
                return await context.UserToProjects.Where(p => p.ProjectId == projectId).Select(p => p.AccountId).ToListAsync();
            }
        }
    }
}
