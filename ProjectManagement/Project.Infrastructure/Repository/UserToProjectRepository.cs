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

                foreach (var proj in project)
                {
                    projectList.Add(proj.Project);
                }

                return projectList;
            }
        }

        public async Task<List<long>> GetAccountIdsByProjectId(long projectId)
        {
            using (var context = new TaskManagementContext())
            {
                return await context.UserToProjects.Where(p => p.ProjectId == projectId).Select(p => p.AccountId).ToListAsync();
            }
        }

        public async Task AddProject(long ownerId, long projectId)
        {
            using (var context = new TaskManagementContext())
            {
                context.UserToProjects.Add(new UserToProject()
                {
                    AccountId = ownerId,
                    ProjectId = projectId
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task AddMember(long projectId, long accountId)
        {
            using (var context = new TaskManagementContext())
            {
                context.UserToProjects.Add(new UserToProject()
                {
                    ProjectId = projectId,
                    AccountId = accountId
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveProjectMember(long projectId, long accountId)
        {
            using (var context = new TaskManagementContext())
            {
                var user = await context.UserToProjects.FirstOrDefaultAsync(u => u.ProjectId == projectId && u.AccountId == accountId);

                context.UserToProjects.Remove(user);

                await context.SaveChangesAsync();
            }
        }
    }
}
