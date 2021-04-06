using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public class UserToTaskService : IUserToTaskService
    {
        public Task<List<Models.Task>> GetTasksByAccountId(long accountId, string token)
        {
            throw new NotImplementedException();
        }
    }
}
