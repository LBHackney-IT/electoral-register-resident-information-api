using System;
using AutoFixture;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Gateways;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.Gateways
{
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
        public void GetResidentByIdReturnsNullIfResidentDoesntExist()
        {
            var response = _classUnderTest.GetEntityById(123);

            response.Should().BeNull();
        }

        [Test]
        public void GetResidentByIdReturnsTheResidentIfFound()
        {
            var (elector, electorExtension, property) = SaveElectorAndAssociatedEntitiesToDatabase();

            var response = _classUnderTest.GetEntityById(elector.Id);

            response.Id.Should().Be(elector.Id);
            response.Should().BeEquivalentTo(new Resident
            {
                Id = elector.Id,
                Email = elector.Email,
                Nationality = elector.Nationality,
                Title = elector.Title,
                Uprn = Convert.ToInt32(property.Uprn),
                FirstName = elector.FirstName,
                MiddleName = electorExtension.MiddleName,
                LastName = elector.LastName,
                DateOfBirth = electorExtension.DateOfBirth
            });
        }

        private (Elector elector, ElectorExtension electorExtension, ElectorsProperty property) SaveElectorAndAssociatedEntitiesToDatabase()
        {
            var property = _fixture.Build<ElectorsProperty>()
                .With(e => e.Uprn, _fixture.Create<int>().ToString).Create();
            ElectoralRegisterContext.Properties.Add(property);
            ElectoralRegisterContext.SaveChanges();

            var elector = _fixture.Build<Elector>()
                .Without(e => e.ElectorsProperty)
                .Without(e => e.ElectorExtension)
                .With(e => e.PropertyId, property.Id)
                .Create();
            ElectoralRegisterContext.Electors.Add(elector);
            ElectoralRegisterContext.SaveChanges();

            var electorExtension = _fixture.Build<ElectorExtension>()
                .With(e => e.Id, elector.Id)
                .Create();
            ElectoralRegisterContext.ElectorExtensions.Add(electorExtension);
            ElectoralRegisterContext.SaveChanges();
            return (elector, electorExtension, property);
        }
    }
}
