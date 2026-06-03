using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace MiniECommerce
{
    public class AuthorizationDataSeederContributor
    : IDataSeedContributor, ITransientDependency
    {
        private readonly IIdentityRoleRepository _roleRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ILookupNormalizer _lookupNormalizer;

        public AuthorizationDataSeederContributor(IIdentityRoleRepository roleRepository, IGuidGenerator guidGenerator, ILookupNormalizer lookupNormalizer)
        {
            _roleRepository = roleRepository;
            _guidGenerator = guidGenerator;
            _lookupNormalizer = lookupNormalizer;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            const string customerRoleName = "Customer";

            // Check if role exists
            var customerRole = await _roleRepository.FindByNormalizedNameAsync(_lookupNormalizer.NormalizeName(customerRoleName));

            if (customerRole == null)
            {
                // Create the new role
                customerRole = new IdentityRole(_guidGenerator.Create(), customerRoleName)
                {
                    IsPublic = true,
                    // Users can see this role
                    IsStatic = true // Allow it to be edited/deleted
                };
                await _roleRepository.InsertAsync(customerRole);
            }
        }
    }
}
