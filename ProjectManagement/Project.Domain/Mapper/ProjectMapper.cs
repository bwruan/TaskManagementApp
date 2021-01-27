using CoreAccount = Project.Domain.Models.Account;
using UserAccount = Project.Infrastructure.UserManagement.Models.Account;
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
            coreProj.OwnerAccount.Id = dbProject.OwnerAccountId;
            
            return coreProj;
        }

        public static CoreAccount UserAccountToCoreAccount(UserAccount userAccount)
        {
            var coreAccount = new CoreAccount();

            coreAccount.Id = userAccount.Id;
            coreAccount.Name = userAccount.Name;
            coreAccount.Email = userAccount.Email;
            coreAccount.RoleId = userAccount.RoleId;

            return coreAccount;
        }
    }
}
