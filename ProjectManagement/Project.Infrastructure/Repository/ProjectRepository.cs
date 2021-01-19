using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        public async Task CreateProject(string name, string description, long ownerId)
        {
            using(var context = new TaskManagementContext())
            {
                context.Projects.Add(new Entities.Project()
                {
                    ProjectName = name,
                    ProjectDescription = description,
                    OwnerAccountId = ownerId
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<Entities.Project> GetProjectByName(string name)
        {
            using(var context = new TaskManagementContext())
            {
                var project = await context.Projects.FirstOrDefaultAsync(p => p.ProjectName == name);

                return project;
            }
        }

        public async Task UpdateProject(long id, string newName, string newDescription)
        {
            using (var context = new TaskManagementContext())
            {
                var project = await context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);

                project.ProjectName = newName;
                project.ProjectDescription = newDescription;
                project.UpdatedDate = DateTime.Now;

                await context.SaveChangesAsync();
            }
        }
    }
}
