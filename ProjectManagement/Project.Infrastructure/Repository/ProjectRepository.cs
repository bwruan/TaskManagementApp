using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Repository.Entities;
using System;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        public async Task<long> CreateProject(string name, string description, long ownerId, DateTime startDate, DateTime endDate)
        {
            using(var context = new TaskManagementContext())
            {
                var project = new Entities.Project()
                {
                    ProjectName = name,
                    ProjectDescription = description,
                    OwnerAccountId = ownerId,
                    StartDate = startDate,
                    EndDate = endDate
                };

                context.Projects.Add(project);

                await context.SaveChangesAsync();

                return project.ProjectId;
            }
        }

        public async Task DeleteProject(long projectId)
        {
            using(var context = new TaskManagementContext())
            {
                var project = await context.Projects.FirstOrDefaultAsync(p => p.ProjectId == projectId);

                context.Projects.Remove(project);

                await context.SaveChangesAsync();
            }
        }

        public async Task<Entities.Project> GetProjectById(long id)
        {
            using(var context = new TaskManagementContext())
            {
                return await context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
            }
        }

        public async Task<Entities.Project> GetProjectByName(string name)
        {
            using(var context = new TaskManagementContext())
            {
                return await context.Projects.FirstOrDefaultAsync(p => p.ProjectName == name);
            }
        }

        public async Task UpdateProject(long projectId, string newName, string newDescription, long newOwnerId)
        {
            using (var context = new TaskManagementContext())
            {
                var project = await context.Projects.FirstOrDefaultAsync(p => p.ProjectId == projectId);

                project.ProjectName = newName;
                project.ProjectDescription = newDescription;
                project.OwnerAccountId = newOwnerId;
                project.UpdatedDate = DateTime.Now;

                await context.SaveChangesAsync();
            }
        }
    }
}
