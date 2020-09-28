using AutoFixture;
using ElectoralRegisterResidentInformationApi.Tests.V1.Helper;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Gateways;
using FluentAssertions;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.Gateways
{
    //TODO: Rename Tests to match gateway name
    //For instruction on how to run tests please see the wiki: https://github.com/LBHackney-IT/lbh-base-api/wiki/Running-the-test-suite.
    [TestFixture]
    public class ElectoralRegisterGatewayTests : DatabaseTests
    {
        private readonly Fixture _fixture = new Fixture();
        private ElectoralRegisterGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new ElectoralRegisterGateway(ElectoralRegisterContext);
        }

        [Test]
        public void GetEntityByIdReturnsNullIfEntityDoesntExist()
        {
            var response = _classUnderTest.GetEntityById(123);

            response.Should().BeNull();
        }

        [Test]
        public void GetEntityByIdReturnsTheEntityIfItExists()
        {
            var entity = _fixture.Create<Resident>();
            var databaseEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);

            ElectoralRegisterContext.Electors.Add(databaseEntity);
            ElectoralRegisterContext.SaveChanges();

            var response = _classUnderTest.GetEntityById(databaseEntity.Id);

            databaseEntity.Id.Should().Be(response.Id);
        }

        //TODO: Add tests here for the get all method.
    }
}
