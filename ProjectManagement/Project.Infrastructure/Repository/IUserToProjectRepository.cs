using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public interface IUserToProjectRepository
    {
        Task<List<Entities.Project>> GetProjectsByAccountId(long id);
    }
}
