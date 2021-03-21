using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public interface IProjectRepository
    {
        Task CreateProject(string name, string description, long ownerId);

        Task UpdateProject(long projectId, string newName, string newDescription, long newOwnerId);

        Task<Entities.Project> GetProjectByName(string name);

        Task<Entities.Project> GetProjectById(long id);
    }
}
