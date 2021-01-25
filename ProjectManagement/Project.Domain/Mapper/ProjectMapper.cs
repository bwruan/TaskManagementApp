using CoreProject = Project.Domain.Models.Project;
using DbProject = Project.Infrastructure.Repository.Entities.Project;

namespace Project.Domain.Mapper
{
    public static class ProjectMapper
    {
        public static CoreProject DbProjectToCoreProject(DbProject dbProject)
        {
            var coreProj = new CoreProject();

            coreProj.ProjectId = dbProject.ProjectId;
            coreProj.ProjectName = dbProject.ProjectName;
            coreProj.ProjectDescription = dbProject.ProjectDescription;
            //map owner id
            return coreProj;
        }
    }
}
