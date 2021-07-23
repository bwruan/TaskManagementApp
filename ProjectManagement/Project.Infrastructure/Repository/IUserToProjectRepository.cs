using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public interface IUserToProjectRepository
    {
        Task<List<Entities.Project>> GetProjectsByAccountId(long accountId);

        Task<List<long>> GetAccountIdsByProjectId(long projectId);

        Task AddProject(long ownerId, long projectId);

        Task AddMember(long projectId, long accountId);

        Task RemoveProjectMember(long projectId, long accountId);

        Task DeleteProject(long projectId);
    }
}
