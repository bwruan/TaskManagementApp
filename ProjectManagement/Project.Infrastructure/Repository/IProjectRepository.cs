using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public interface IProjectRepository
    {
        Task CreateProject(string name, string description, long ownerId);

        Task UpdateProject(long id, string newName, string newDescription);

        Task<Entities.Project> GetProjectByName(string name);
    }
}
