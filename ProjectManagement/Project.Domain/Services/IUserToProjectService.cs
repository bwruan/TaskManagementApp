using Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Services
{
    public interface IUserToProjectService
    {
        Task<List<Models.Project>> GetProjectsByAccountId(long accountId, string token);

        Task<List<Account>> GetAccountByProjectId(long projectId, string token);

        Task AddProject(string name, string description, long ownerId, DateTime startDate, DateTime endDate, long projectId);

        Task<Account> AddMember(long projectId, string email, string token);

        Task RemoveProjectMember(long projectId, long accountId, string token);
    }
}
