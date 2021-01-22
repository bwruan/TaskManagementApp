using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Services
{
    public interface IUserToProjectService
    {
        Task<List<Models.Project>> GetProjectsByAccountId(long accountId);

        Task<List<long>> GetAccountByProjectId(long projectId);
    }
}
