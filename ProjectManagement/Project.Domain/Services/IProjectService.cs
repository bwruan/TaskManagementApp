using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Services
{
    public interface IProjectService
    {
        Task<long> CreateProject(string name, string description, long ownerId, DateTime startDate, DateTime endDate);

        Task UpdateProject(long projectId, string newName, string newDescription, long newOwnerId);

        Task<Models.Project> GetProjectByName(string name, string token);

        Task<Models.Project> GetProjectById(long id, string token);
    }
}
