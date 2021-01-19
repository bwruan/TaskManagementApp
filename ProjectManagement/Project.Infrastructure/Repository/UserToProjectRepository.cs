using Project.Infrastructure.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public class UserToProjectRepository : IUserToProjectRepository
    {
        //how to get Account Info from User Service???
        public async Task<List<Entities.Project>> GetProjectsByAccountId(long id)
        {
            //using(var context = new TaskManagementContext())
            //{
            //    var account = await context.Account.FirstOrDefaultAsync(a => a.Id.Equals(id));

            //    var project = await context.UserToProjects.Where(p => p.AccountId == account.Id).ToListAsync();

                var projectList = new List<Entities.Project>();

            //    var dbProjects = await context.Account.Where(a => project.Contains(a.Id)).ToListAsync();

            //    foreach(var dbProj in dbProjects)
            //    {
            //        projectList.Add(dbProj);
            //    }

                return projectList;
            //}
        }
    }
}
