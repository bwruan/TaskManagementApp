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
        //how to get Account Info from User Service??? - don't worry about doing this in the repository. that is something else.
        //in repository, u need to just return the projects.
        public async Task<List<Entities.Project>> GetProjectsByAccountId(long id)
        {
            using (var context = new TaskManagementContext())
            {
                //instead of using account.Id, what can u use? Once usccessful this will return u a list of UserToProjects objects tied to the id
                //UserToProjects object has a Project property that will return u the project tied to it, but in order to do that, you'll need to use include in linq.
                //otherwise, if u dont include it, the project property will be null
                var project = await context.UserToProjects.Where(p => p.AccountId == account.Id).ToListAsync(); 

                var projectList = new List<Entities.Project>();

                var dbProjects = await context.Account.Where(a => project.Contains(a.Id)).ToListAsync(); //again no need to worry about accounts here

                //instead of looping through dbProjects, what can u loop through?
                foreach (var dbProj in dbProjects) 
                {
                    projectList.Add(dbProj);
                }

                return projectList;
            }
        }

        //should create a method for GetAccountsByProjectId too right? Again, dont worry about returning account object, you can reutrn the account ids
        //then with the account ids returned, in ur service is wqehre ull make a call to get account info using the id from user service.
        //for the repo, just focus on retrieving whatever info is available to u at the db level.
    }
}
