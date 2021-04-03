using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTask.Infrastructure.Repositories
{
    public class UserToTaskRepository : IUserToTaskRepository
    {
        public async Task<List<Entities.Task>> GetTasksByAccountId(long accountId)
        {
            using (var context = new Entities.TaskManagementContext())
            {
                var tasks = await context.UserToTasks.Include(t => t.Task).Where(t => t.AccountId == accountId).ToListAsync();

                var taskList = new List<Entities.Task>();

                foreach(var task in tasks)
                {
                    taskList.Add(task.Task);
                }

                return taskList;
            }
        }
    }
}
