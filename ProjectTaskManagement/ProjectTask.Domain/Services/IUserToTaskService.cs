using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public interface IUserToTaskService
    {
        Task<List<Models.Task>> GetTasksByAccountId(long accountId, string token);
    }
}
