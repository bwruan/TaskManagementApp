using DbAccout = User.Infrastructure.Repository.Entities.Account;
using CoreAccount = User.Domain.Models.Account;

namespace User.Domain.Mapper
{
    public static class AccountMapper
    {
        public static CoreAccount DbAccountToCoreAccount(DbAccout dbAccout)
        {
            var coreAccount = new CoreAccount();

            coreAccount.Id = dbAccout.Id;
            coreAccount.Name = dbAccout.Name;
            coreAccount.Email = dbAccout.Email;
            coreAccount.Password = dbAccout.Password;
            coreAccount.RoleId = dbAccout.RoleId;
            coreAccount.ProfilePic = dbAccout.ProfilePic;
            coreAccount.Status = dbAccout.Status;

            return coreAccount;
        }
    }
}
