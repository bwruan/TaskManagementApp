using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Infrastructure.Repositories
{
    public interface IUserToTaskRepository
    {
        Task <List<Entities.Task>> GetTasksByAccountId(long accountId);
    }
}
