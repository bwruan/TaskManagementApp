﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Services
{
    public interface IProjectService
    {
        Task CreateProject(string name, string description, long ownerId);

        Task UpdateProject(long projectId, string newName, string newDescription, long newOwnerId);

        Task<Models.Project> GetProjectByName(string name);

        Task<Models.Project> GetProjectById(long id);
    }
}
