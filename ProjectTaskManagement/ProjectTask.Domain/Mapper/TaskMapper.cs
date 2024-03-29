﻿using CoreTask = ProjectTask.Domain.Models.Task;
using DbTask = ProjectTask.Infrastructure.Repositories.Entities.Task;
using CoreProject = ProjectTask.Domain.Models.Project;
using CurrentProject = ProjectTask.Infrastructure.ProjectManagement.Models.Project;
using CoreAccount = ProjectTask.Domain.Models.Account;
using ServiceAccount = ProjectTask.Infrastructure.UserManagement.Models.Account;

namespace ProjectTask.Domain.Mapper
{
    public class TaskMapper
    {
        public static CoreTask DbTaskToCoreTask(DbTask dbTask)
        {
            var coreTask = new CoreTask();

            coreTask.TaskId = dbTask.TaskId;
            coreTask.TaskName = dbTask.TaskName;
            coreTask.TaskDescription = dbTask.TaskDescription;
            coreTask.ProjectId = dbTask.ProjectId;
            coreTask.TaskeeAccount = new CoreAccount() { Id = dbTask.Taskee.Id, Name = dbTask.Taskee.Name };
            coreTask.DueDate = dbTask.DueDate;
            coreTask.CreatedDate = dbTask.CreatedDate;
            coreTask.UpdatedDate = dbTask.UpdatedDate;
            coreTask.CompletedDate = dbTask.CompletedDate;
            coreTask.IsCompleted = dbTask.IsCompleted;

            return coreTask;
        }

        public static CoreProject CurrentProjectToCoreProject(CurrentProject currentProject)
        {
            var coreProject = new CoreProject();

            coreProject.AccountId = currentProject.AccountId;
            coreProject.ProjectDescription = currentProject.ProjectDescription;
            coreProject.ProjectId = currentProject.ProjectId;
            coreProject.ProjectName = currentProject.ProjectName;

            return coreProject;
        }

        public static CoreAccount ServiceAccountToCoreAccount(ServiceAccount serviceAccount)
        {
            var coreAccount = new CoreAccount();

            coreAccount.Id = serviceAccount.Id;
            coreAccount.Name = serviceAccount.Name;
            coreAccount.RoleId = serviceAccount.RoleId;
            coreAccount.RoleName = serviceAccount.RoleName;
            coreAccount.Email = serviceAccount.Email;
            coreAccount.Status = serviceAccount.Status;

            return coreAccount;
        }
    }
}
