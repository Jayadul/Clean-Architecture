﻿using CA.Core.Domain.Identity.Contracts;
using CA.Core.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CA.Infrastructure.Identity.Managers
{
    public class ApplicationRoleManager : IApplicationRoleManager
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationRoleManager(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IList<Claim>> GetClaims(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return await _roleManager.GetClaimsAsync(role);
        }

        public async Task<IList<Claim>> GetClaims(IList<string> roleNames)
        {
            var roles = _roleManager.Roles.Where(x => roleNames.Contains(x.Name)).ToList();
            var allClaims = new List<Claim>();
            
            foreach (var role in roles)
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                allClaims.AddRange(claims);
            }
            return allClaims;
        }

        public async Task<ApplicationRole> GetRole(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public IQueryable<ApplicationRole> Roles()
        {
            return _roleManager.Roles;
        }
    }
}
