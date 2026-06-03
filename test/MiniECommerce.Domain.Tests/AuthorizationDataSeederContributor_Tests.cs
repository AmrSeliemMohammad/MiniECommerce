using Microsoft.AspNetCore.Identity;
using MiniECommerce.Products;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Xunit;

namespace MiniECommerce
{
    public class AuthorizationDataSeederContributor_Tests
    {
        private readonly IIdentityRoleRepository _fakeRoleRepository;
        private readonly IGuidGenerator _fakeGuidGenerator;
        private readonly ILookupNormalizer _fakeLookupNormalizer;
        public AuthorizationDataSeederContributor_Tests()
        {
            _fakeRoleRepository = Substitute.For<IIdentityRoleRepository>();
            _fakeGuidGenerator = Substitute.For<IGuidGenerator>();
            _fakeLookupNormalizer = Substitute.For<ILookupNormalizer>();
        }

        [Fact]
        public async Task Should_Seed_Customer_Role_If_Not_Exists()
        {
            //Arrange
            _fakeRoleRepository.FindByNormalizedNameAsync(Arg.Any<string>()).ReturnsNull();
            _fakeLookupNormalizer.NormalizeName(Arg.Any<string>()).Returns(TestData.CustomerRoleNormalizedName);
            _fakeGuidGenerator.Create().Returns(Guid.NewGuid());

            var dataSeederContributor = new AuthorizationDataSeederContributor(_fakeRoleRepository, _fakeGuidGenerator, _fakeLookupNormalizer);
            //Act
            await dataSeederContributor.SeedAsync(null);
            //Assert
            await _fakeRoleRepository.Received(1).FindByNormalizedNameAsync(TestData.CustomerRoleNormalizedName);
            await _fakeRoleRepository.Received(1).InsertAsync(Arg.Is<IdentityRole>(role => role.Name == TestData.CustomerRoleName && role.IsPublic && role.IsStatic));
        }

        [Fact]
        public async Task Should_Do_Nothing_If_Role_Exists()
        {
            _fakeRoleRepository.FindByNormalizedNameAsync(Arg.Any<string>()).Returns(new IdentityRole(Guid.NewGuid(), TestData.CustomerRoleName));
            _fakeLookupNormalizer.NormalizeName(Arg.Any<string>()).Returns(TestData.CustomerRoleNormalizedName);

            var dataSeederContributor = new AuthorizationDataSeederContributor(_fakeRoleRepository, _fakeGuidGenerator, _fakeLookupNormalizer);
            //Act
            await dataSeederContributor.SeedAsync(null);
            //Assert
            await _fakeRoleRepository.Received(1).FindByNormalizedNameAsync(TestData.CustomerRoleNormalizedName);
            await _fakeRoleRepository.DidNotReceive().InsertAsync(Arg.Is<IdentityRole>(role => role.Name == TestData.CustomerRoleName && role.IsPublic && role.IsStatic));
        }
    }
}
