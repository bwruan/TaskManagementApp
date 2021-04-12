using ProjectTask.Domain.Mapper;
using ProjectTask.Infrastructure.Repositories;
using ProjectTask.Infrastructure.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTask.Domain.Services
{
    public class UserToTaskService : IUserToTaskService
    {
        private readonly IUserToTaskRepository _userToTaskRepository;
        private readonly IUserService _userService;

        public UserToTaskService(IUserToTaskRepository userToTaskRepository, IUserService userService)
        {
            _userToTaskRepository = userToTaskRepository;
            _userService = userService;
        }

        public async Task<List<Models.Task>> GetTasksByAccountId(long accountId, string token)
        {
            var taskList = new List<Models.Task>();

            var tasks = await _userToTaskRepository.GetTasksByAccountId(accountId);

            foreach(var task in tasks)
            {
                var account = await _userService.GetAccountById(task.TaskerId, token);
                var coreTask = TaskMapper.DbTaskToCoreTask(task);

                coreTask.TaskerAccount = TaskMapper.TaskerAccountToCoreAccount(account);

                taskList.Add(coreTask);
            }

            return taskList;
        }
    }
}
