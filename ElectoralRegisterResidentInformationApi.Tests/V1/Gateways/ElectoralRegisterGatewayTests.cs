using System;
using System.Linq;
using AutoFixture;
using ElectoralRegisterResidentInformationApi.V1.Domain;
using ElectoralRegisterResidentInformationApi.V1.Gateways;
using ElectoralRegisterResidentInformationApi.V1.Infrastructure;
using ElectoralRegisterResidentInformationApi.V1.Boundary.Request;
using FluentAssertions;
using NUnit.Framework;
using Bogus;

namespace ElectoralRegisterResidentInformationApi.Tests.V1.Gateways
{
    [TestFixture]
    public class ElectoralRegisterGatewayTests : DatabaseTests
    {
        private readonly Fixture _fixture = new Fixture();
        private ElectoralRegisterGateway _classUnderTest;
        private readonly Faker _faker = new Faker();

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

        [Test]
        public void GetAllResidentsIfThereAreNoResidentsReturnsAnEmptyList()
        {
            var response = _classUnderTest.GetAllResidents(new ListResidentsRequest());

            response.Should().BeEmpty();
        }

        [Test]
        public void GetAllResidentsWillRetrieveElectorRecordsAndRelatedData()
        {
            var dateOfBirthForOne = _faker.Date.Past();
            var dateOfBirthForTwo = _faker.Date.Past();
            var firstName = _faker.Person.FirstName;
            var (electorOne, electorExtensionOne, propertyOne) = SaveElectorAndAssociatedEntitiesToDatabase(dateOfBirthForOne, firstName, 1);
            var (electorTwo, electorExtensionTwo, propertyTwo) = SaveElectorAndAssociatedEntitiesToDatabase(dateOfBirthForTwo, firstName, 2);
            SaveElectorAndAssociatedEntitiesToDatabase(dateOfBirthForTwo); //add a resident to db that should not be returned

            var request = new ListResidentsRequest() { FirstName = electorOne.FirstName };
            //---- Call gateway method
            var listOfRecords = _classUnderTest.GetAllResidents(request);

            var residentOne = listOfRecords.First();
            var residentTwo = listOfRecords.Last();

            listOfRecords.Count.Should().Be(2);
            residentOne.Id.Should().Be(electorOne.Id);

            residentOne.Should().BeEquivalentTo(new Resident
            {
                Id = electorOne.Id,
                Email = electorOne.Email,
                Nationality = electorOne.Nationality,
                Title = electorOne.Title,
                Uprn = Convert.ToInt32(propertyOne.Uprn),
                FirstName = electorOne.FirstName,
                MiddleName = electorExtensionOne.MiddleName,
                LastName = electorOne.LastName,
                DateOfBirth = electorExtensionOne.DateOfBirth
            });

            residentTwo.Id.Should().Be(electorTwo.Id);

            residentTwo.Should().BeEquivalentTo(new Resident
            {
                Id = electorTwo.Id,
                Email = electorTwo.Email,
                Nationality = electorTwo.Nationality,
                Title = electorTwo.Title,
                Uprn = Convert.ToInt32(propertyTwo.Uprn),
                FirstName = electorTwo.FirstName,
                MiddleName = electorExtensionTwo.MiddleName,
                LastName = electorTwo.LastName,
                DateOfBirth = electorExtensionTwo.DateOfBirth
            });
        }

        private (Elector elector, ElectorExtension electorExtension, ElectorsProperty property) SaveElectorAndAssociatedEntitiesToDatabase(DateTime? birthdate = null,
            string firstName = null, int id = 0)
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

            elector.FirstName = firstName ?? elector.FirstName;
            if (id != 0) { elector.Id = id; }
            ElectoralRegisterContext.Electors.Add(elector);
            ElectoralRegisterContext.SaveChanges();

            var electorExtension = _fixture.Build<ElectorExtension>()
                .With(e => e.Id, elector.Id)
                .With(e => e.DateOfBirth, birthdate)
                .Create();
            ElectoralRegisterContext.ElectorExtensions.Add(electorExtension);
            ElectoralRegisterContext.SaveChanges();
            return (elector, electorExtension, property);
        }
    }
}
