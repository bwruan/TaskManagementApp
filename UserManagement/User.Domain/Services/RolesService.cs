using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Domain.Mapper;
using User.Domain.Models;
using User.Infrastructure.Repository;

namespace User.Domain.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task AddRole(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Role Name is blank.");
            }

            await _rolesRepository.AddRole(name);
        }

        public async Task<List<Roles>> GetRoles()
        {
            var rolesList = new List<Roles>();

            var dbRolesList = await _rolesRepository.GetRoles();

            foreach(var roles in dbRolesList)
            {
                rolesList.Add(RolesMapper.DbRolesToCoreRoles(roles));
            }

            return rolesList;
        }
    }
}
