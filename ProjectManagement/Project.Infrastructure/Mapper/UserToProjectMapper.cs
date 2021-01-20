using EntUserToProject = Project.Infrastructure.Repository.Entities.UserToProject;
using EntProject = Project.Infrastructure.Repository.Entities.Project;

namespace Project.Infrastructure.Mapper
{
    public static class UserToProjectMapper
    {
        public static EntProject UserToProjectToProject(EntUserToProject userToProject)
        {
            var project = new EntProject();

            project.OwnerAccountId = userToProject.Project.OwnerAccountId;
            project.ProjectId = userToProject.Project.ProjectId;
            project.ProjectName = userToProject.Project.ProjectName;
            project.ProjectDescription = userToProject.Project.ProjectDescription;

            return project;
        }
    }
}
