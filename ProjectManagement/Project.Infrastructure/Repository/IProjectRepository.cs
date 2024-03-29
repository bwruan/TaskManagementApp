﻿using System;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public interface IProjectRepository
    {
        Task<long> CreateProject(string name, string description, long ownerId, DateTime startDate, DateTime endDate);

        Task UpdateProject(long projectId, string newName, string newDescription, long newOwnerId);

        Task<Entities.Project> GetProjectByName(string name);

        Task<Entities.Project> GetProjectById(long id);

        Task DeleteProject(long projectId);
    }
}
