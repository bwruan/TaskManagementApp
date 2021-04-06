﻿using CoreTask = ProjectTask.Domain.Models.Task;
using DbTask = ProjectTask.Infrastructure.Repositories.Entities.Task;
using CoreProject = ProjectTask.Domain.Models.Project;
using CurrentProject = ProjectTask.Infrastructure.ProjectManagement.Models.Project;

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
            coreTask.DueDate = dbTask.DueDate;
            coreTask.CompletedDate = dbTask.CompletedDate;
            coreTask.CreatedDate = dbTask.CreatedDate;
            coreTask.UpdatedDate = dbTask.UpdatedDate;

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
    }
}