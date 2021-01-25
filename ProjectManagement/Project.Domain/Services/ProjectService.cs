using Project.Domain.Mapper;
using Project.Infrastructure.Repository;
using System;
using System.Threading.Tasks;

namespace Project.Domain.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task CreateProject(string name, string description, long ownerId)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Project Name field blank");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Description field blank");
            }

            //should probably make sure that owner Id is atleast > 0 because in the DB, PK starts at 1. 
            //so if owner id is <= 0, we know something is wrong.
            
            await _projectRepository.CreateProject(name, description, ownerId);
        }

        public async Task<Models.Project> GetProjectById(long id)
        {
            var project = await _projectRepository.GetProjectById(id);

            if(project == null)
            {
                throw new ArgumentException("Project does not exist");
            }
            //this will map DB entity to core model entity.

            //before returning, you should use the USER Service to get user by Id (owner id returned from DB). Then populate the account property of your core model
            //from what is returned by user service.
            return ProjectMapper.DbProjectToCoreProject(project);
        }

        public async Task<Models.Project> GetProjectByName(string name)
        {
            var project = await _projectRepository.GetProjectByName(name);

            if(project == null)
            {
                throw new ArgumentException("Project does not exist.");
            }

            //before returning, you should use the USER Service to get user by Id (owner id returned from DB). Then populate the account property of your core model
            //from what is returned by user service.
            return ProjectMapper.DbProjectToCoreProject(project);
        }

        public async Task UpdateProject(long projectId, string newName, string newDescription, long newOwnerId)
        {
            var project = await _projectRepository.GetProjectById(projectId);

            if(project == null)
            {
                throw new ArgumentException("Project does not exist.");
            }

            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentException("Project Name field blank");
            }

            if (string.IsNullOrEmpty(newDescription))
            {
                throw new ArgumentException("Project Description field blank");
            }

            //check to make sure newOwnerId is > 0 as well. reason listed above

            await _projectRepository.UpdateProject(projectId, newName, newDescription, newOwnerId);
        }
    }
}
